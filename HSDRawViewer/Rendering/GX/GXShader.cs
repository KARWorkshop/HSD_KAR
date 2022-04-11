﻿using System;
using OpenTK.Graphics.OpenGL;
using System.ComponentModel;
using YamlDotNet.Serialization;
using OpenTK;

namespace HSDRawViewer.Rendering.GX
{
    public class GXShader
    {
        // Shader
        public static Shader _shader;

        [YamlIgnore, Browsable(false)]
        public RenderMode RenderMode { get; set; }


        [YamlIgnore, Browsable(false)]
        public int SelectedBone { get; set; }

        private Matrix4[] WorldTransforms = new Matrix4[400];

        /// <summary>
        /// 
        /// </summary>
        /// <param name="world"></param>
        /// <param name="bind"></param>
        public void SetBoneTransforms(Matrix4[] world, Matrix4[] bind)
        {
            Array.Copy(world, WorldTransforms, world.Length);
            Array.Copy(bind, 0, WorldTransforms, Shader.MAX_BONES, bind.Length);
        }

        /// <summary>
        /// 
        /// </summary>
        public void Bind(Camera camera, GXLightParam light, GXFogParam fog)
        {
            // load shader if it's not ready yet
            if (_shader == null)
            {
                _shader = new Shader();
                _shader.LoadShader(System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"Shader\gx.vert"));
                _shader.LoadShader(System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"Shader\gx_uv.frag"));
                _shader.LoadShader(System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"Shader\gx_lightmap.frag"));
                _shader.LoadShader(System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"Shader\gx_material.frag"));
                _shader.LoadShader(System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"Shader\gx_alpha_test.frag"));
                _shader.LoadShader(System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"Shader\gx.frag"));
                _shader.Link();

                if (!_shader.ProgramCreatedSuccessfully())
                    System.Windows.Forms.MessageBox.Show("Shader failed to link or compile");
            }

            if (!_shader.ProgramCreatedSuccessfully())
                return;

            // bind shader
            GL.UseProgram(_shader.programId);


            // load model view matrix
            var mvp = camera.MvpMatrix;
            GL.UniformMatrix4(_shader.GetVertexAttributeUniformLocation("mvp"), false, ref mvp);

            // set camera position
            var campos = (camera.RotationMatrix * new Vector4(camera.Translation, 1)).Xyz;
            _shader.SetVector3("cameraPos", campos);

            // create sphere matrix
            Matrix4 sphereMatrix = camera.ModelViewMatrix;
            sphereMatrix.Invert();
            sphereMatrix.Transpose();
            _shader.SetMatrix4x4("sphereMatrix", ref sphereMatrix);

            // ui
            _shader.SetInt("selectedBone", SelectedBone);
            _shader.SetInt("renderOverride", (int)RenderMode);

            // setup bone binds
            _shader.SetWorldTransformBones(WorldTransforms);
            // _shader.SetBindTransformBones(BindTransforms);

            //var tb = BindTransforms;
            //if (tb.Length > 0)
            //    _shader.SetMatrix4x4("binds", tb);

            // lighting
            light.Bind(_shader);

            // fog
            fog.Bind(_shader);
        }

        /// <summary>
        /// 
        /// </summary>
        public void Unbind()
        {
            GL.UseProgram(0);
        }

    }
}
