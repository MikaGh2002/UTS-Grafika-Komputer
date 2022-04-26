using LearnOpenTK.Common;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _3D_Object
{
    class Asset3d
    {
        private readonly string path = "../../../Shaders/";

        public List<Vector3> vertices = new List<Vector3>();
        private List<uint> indices = new List<uint>();

        private int _vertexBufferObject;
        private int _vertexArrayObject;
        private int _elementBufferObject;

        private Shader _shader;

        private Matrix4 model = Matrix4.Identity;
        private Matrix4 view;
        private Matrix4 projection;

        private Vector3 color;

        public List<Vector3> _euler = new List<Vector3>();
        public Vector3 objectCenter = Vector3.Zero;

        public List<Asset3d> child = new List<Asset3d>();

        public int counter = 0;
        public Asset3d(Vector3 color)
        {
            this.color = color;
            _euler.Add(Vector3.UnitX);
            _euler.Add(Vector3.UnitY);
            _euler.Add(Vector3.UnitZ);
        }

        public void load(int sizeX, int sizeY)
        {
            _vertexBufferObject = GL.GenBuffer();

            GL.BindBuffer(BufferTarget.ArrayBuffer, _vertexBufferObject);

            GL.BufferData(BufferTarget.ArrayBuffer, vertices.Count * Vector3.SizeInBytes, vertices.ToArray(), BufferUsageHint.StaticDraw);

            _vertexArrayObject = GL.GenVertexArray();
            GL.BindVertexArray(_vertexArrayObject);

            GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, 3 * sizeof(float), 0);
            GL.EnableVertexAttribArray(0);

            if (indices.Count != 0)
            {
                _elementBufferObject = GL.GenBuffer();
                GL.BindBuffer(BufferTarget.ElementArrayBuffer, _elementBufferObject);
                GL.BufferData(BufferTarget.ElementArrayBuffer, indices.Count * sizeof(uint), indices.ToArray(), BufferUsageHint.StaticDraw);
            }

            view = Matrix4.CreateTranslation(0, 0, -8.0f);
            projection = Matrix4.CreatePerspectiveFieldOfView(MathHelper.DegreesToRadians(45.0f), sizeX / (float)sizeY, 0.01f, 100f);

            _shader = new Shader(path + "shader.vert", path + "shader.frag");
            _shader.Use();

            foreach (var i in child)
            {
                i.load(sizeX, sizeY);
            }
        }

        public void render(Matrix4 camera_view, Matrix4 camera_projection, int pilihan)
        {
            if (pilihan == 1)
            {
                if (counter < 5000)
                {
                    model *= Matrix4.CreateTranslation(new Vector3(0.0f, 0.001f, 0.0f));
                    counter += 1;
                }
                else
                {
                    model *= Matrix4.CreateTranslation(new Vector3(0.0f, -0.001f, 0.0f));
                    counter += 1;
                }

                if (counter == 10000)
                {
                    counter = 0;
                }
            }
            else if (pilihan == 2)
            {
                if (counter < 1000)
                {
                    model *= Matrix4.CreateTranslation(new Vector3(0.0f, 0.0005f, 0.0f));
                    counter += 1;
                }
                else
                {
                    model *= Matrix4.CreateTranslation(new Vector3(0.0f, -0.0005f, 0.0f));
                    counter += 1;
                }

                if (counter == 2000)
                {
                    counter = 0;
                }
            }
            else if (pilihan == 3)
            {
                if (counter < 1000)
                {
                    model *= Matrix4.CreateTranslation(new Vector3(0.0f, -0.0005f, 0.0f));
                    counter += 1;
                }
                else
                {
                    model *= Matrix4.CreateTranslation(new Vector3(0.0f, 0.0005f, 0.0f));
                    counter += 1;
                }

                if (counter == 2000)
                {
                    counter = 0;
                }
            }
            else if (pilihan == 4)
            {
                if (counter < 100)
                {
                    model *= Matrix4.CreateTranslation(new Vector3(0.0f, 0.005f, 0.0f));
                    counter += 1;
                }
                else
                {
                    model *= Matrix4.CreateTranslation(new Vector3(0.0f, -0.005f, 0.0f));
                    counter += 1;
                }

                if (counter == 200)
                {
                    counter = 0;
                }
            }
            _shader.Use();
            GL.BindVertexArray(_vertexArrayObject);

            _shader.SetVector3("objColor", color);

            _shader.SetMatrix4("model", model);
            _shader.SetMatrix4("view", camera_view);
            _shader.SetMatrix4("projection", camera_projection);


            if (indices.Count != 0)
            {
                GL.DrawElements(PrimitiveType.Triangles, indices.Count, DrawElementsType.UnsignedInt, 0);
            }
            else
            {
               GL.DrawArrays(PrimitiveType.LineStrip, 0, vertices.Count );
            }
        }



        public void resetEuler()
        {
            _euler.Clear();
            _euler.Add(Vector3.UnitX);
            _euler.Add(Vector3.UnitY);
            _euler.Add(Vector3.UnitZ);
        }

        public void createBeam(float centerX, float centerZ, float radius)
        {
            Vector3 vec;
            vertices = new List<Vector3>();
            for (int i = 0; i < 360; i++)
            {
                double degInRad = i * Math.PI / 180;
                vec.X = radius * (float)Math.Cos(degInRad) + centerX;
                vec.Y = 2f;
                vec.Z = radius * (float)Math.Sin(degInRad) + centerZ;
                vertices.Add(vec);
            }
            for (int i = 0; i < 360; i++)
            {
                double degInRad = i * Math.PI / 180;
                vec.X = (radius + 0.2f) * (float)Math.Cos(degInRad) + centerX;
                vec.Y = 0;
                vec.Z = (radius + 0.2f) * (float)Math.Sin(degInRad) + centerZ;
                vertices.Add(vec);
            }
            indices = new List<uint>();
            for (uint i = 0; i < 359; i += 2)
            {
                indices.Add(i);
                indices.Add(i + 1);
                indices.Add(i + 361);
            }
        }
        public void createTopi2(float centerX, float centerZ, float radius)
        {
            Vector3 vec;
            vertices = new List<Vector3>();
            for (int i = 0; i < 360; i++)
            {
                double degInRad = i * Math.PI / 180;
                vec.X = radius * (float)Math.Cos(degInRad) + centerX;
                vec.Y = -1.5f;
                vec.Z = radius * (float)Math.Sin(degInRad) + centerZ;
                vertices.Add(vec);
            }
            vec.X = centerX;
            vec.Y = -0.7f;
            vec.Z = centerZ;
            vertices.Add(vec);
            for (int i = 0; i < 360; i++)
            {
                double degInRad = i * Math.PI / 180;
                vec.X = (radius + 0.2f) * (float)Math.Cos(degInRad) + centerX;
                vec.Y = -1.2f;
                vec.Z = (radius + 0.2f) * (float)Math.Sin(degInRad) + centerZ;
                vertices.Add(vec);
            }
            indices = new List<uint>();
            for (uint i = 0; i < 359; i += 1)
            {
                indices.Add(i);
                indices.Add(i + 1);
                indices.Add(360);
            }
            indices.Add(359);
            indices.Add(0);
            indices.Add(360);
            for (uint i = 0; i < 359; i += 1)
            {
                indices.Add(i);
                indices.Add(i + 1);
                indices.Add(i + 361);
            }
            indices.Add(359);
            indices.Add(0);
            indices.Add(720);
            for (uint i = 0; i < 359; i += 1)
            {
                indices.Add(i + 1);
                indices.Add(i + 361);
                indices.Add(i + 361 + 1);
            }
            indices.Add(0);
            indices.Add(720);
            indices.Add(361);
        }
        public void createTopi(float centerX, float centerZ, float radius)
        {
            Vector3 vec;
            vertices = new List<Vector3>();
            for (int i = 0; i < 360; i++)
            {
                double degInRad = i * Math.PI / 180;
                vec.X = radius * (float)Math.Cos(degInRad) + centerX;
                vec.Y = -1.5f;
                vec.Z = radius * (float)Math.Sin(degInRad) + centerZ;
                vertices.Add(vec);
            }
            vec.X = centerX;
            vec.Y = -0.5f;
            vec.Z = centerZ;
            vertices.Add(vec);
            indices = new List<uint>();
            for (uint i = 0; i < 359; i += 2)
            {
                indices.Add(i);
                indices.Add(i + 1);
                indices.Add(360);
            }
        }

        public void createPortal(float centerX, float centerZ, float radius)
        {
            Vector3 vec;
            vertices = new List<Vector3>();
            for (int i = 0; i < 360; i++)
            {
                double degInRad = i * Math.PI / 180;
                vec.X = radius * (float)Math.Cos(degInRad) + centerX;
                vec.Y = 1f;
                vec.Z = radius * (float)Math.Sin(degInRad) + centerZ;
                vertices.Add(vec);
            }
            vec.X = centerX;
            vec.Y = 1.5f;
            vec.Z = centerZ;
            vertices.Add(vec);
            indices = new List<uint>();
            for (uint i = 0; i < 359; i += 1)
            {
                indices.Add(i);
                indices.Add(i + 1);
                indices.Add(360);
            }
            indices.Add(359);
            indices.Add(0);
            indices.Add(360);
        }

        public void createTanduk(float centerX, float centerZ, float radius)
        {
            Vector3 vec;
            vertices = new List<Vector3>();
            for (int i = 0; i < 360; i++)
            {
                double degInRad = i * Math.PI / 180;
                vec.X = radius * (float)Math.Cos(degInRad) + centerX;
                vec.Y = 0.5f;
                vec.Z = radius * (float)Math.Sin(degInRad) + centerZ;
                vertices.Add(vec);
            }
            vec.X = centerX;
            vec.Y = 1.5f;
            vec.Z = centerZ;
            vertices.Add(vec);
            indices = new List<uint>();
            for (uint i = 0; i < 359; i++)
            {
                indices.Add(i);
                indices.Add(i + 1);
                indices.Add(360);
            }
            indices.Add(359);
            indices.Add(0);
            indices.Add(360);
        }
        //createHolo
        public void createAura(float centerX, float centerZ, float radius)
        {
            Vector3 vec;
            vertices = new List<Vector3>();
            for (int i = 0; i < 360; i++)
            {
                double degInRad = i * Math.PI / 180;
                vec.X = radius * (float)Math.Cos(degInRad) + centerX;
                vec.Y = 0f;
                vec.Z = radius * (float)Math.Sin(degInRad) + centerZ;
                vertices.Add(vec);
            }
            for (int i = 0; i < 360; i++)
            {
                double degInRad = i * Math.PI / 180;
                vec.X = (radius + 0.2f) * (float)Math.Cos(degInRad) + centerX;
                vec.Y = 0f;
                vec.Z = (radius + 0.2f) * (float)Math.Sin(degInRad) + centerZ;
                vertices.Add(vec);
            }
            for (int i = 0; i < 360; i++)
            {
                double degInRad = i * Math.PI / 180;
                vec.X = radius * (float)Math.Cos(degInRad) + centerX;
                vec.Y = -2.2f;
                vec.Z = radius * (float)Math.Sin(degInRad) + centerZ;
                vertices.Add(vec);
            }
            for (int i = 0; i < 360; i++)
            {
                double degInRad = i * Math.PI / 180;
                vec.X = (radius + 0.6f) * (float)Math.Cos(degInRad) + centerX;
                vec.Y = -2.2f;
                vec.Z = (radius + 0.6f) * (float)Math.Sin(degInRad) + centerZ;
                vertices.Add(vec);
            }
            indices = new List<uint>();
            for (uint i = 0; i < 359; i++)
            {
                indices.Add(i);
                indices.Add(i + 1);
                indices.Add(i + 361);
            }
            for (uint i = 0; i < 359; i++)
            {
                indices.Add(i + 1);
                indices.Add(i + 360);
                indices.Add(i + 361);
            }
            for (uint i = 0; i < 359; i++)
            {
                indices.Add(i + 720);
                indices.Add(i + 721);
                indices.Add(i + 720 + 361);
            }
            for (uint i = 0; i < 359; i += 2)
            {
                indices.Add(i);
                indices.Add(i + 720);
                indices.Add(i + 721);
            }
        }

        public void createDiamond(float pusatX, float pusatY, float pusatZ)
        {
            Vector3 vec;
            // 0
            vec.X = 0.0f;
            vec.Y = 0.0f;
            vec.Z = 0.0f;
            vertices.Add(vec);
            //1
            vec.X = 0.0f;
            vec.Y = 0.0f;
            vec.Z = 0.2f;
            vertices.Add(vec);
            //2
            vec.X = 0.1f;
            vec.Y = 0.0f;
            vec.Z = 0.1f;
            vertices.Add(vec);
            //3
            vec.X = 0.2f;
            vec.Y = 0.0f;
            vec.Z = 0.0f;
            vertices.Add(vec);
            //4
            vec.X = 0.1f;
            vec.Y = 0.0f;
            vec.Z = -0.1f;
            vertices.Add(vec);
            //5
            vec.X = 0.0f;
            vec.Y = 0.0f;
            vec.Z = -0.2f;
            vertices.Add(vec);
            //6
            vec.X = -0.1f;
            vec.Y = 0.0f;
            vec.Z = -0.1f;
            vertices.Add(vec);
            //7
            vec.X = -0.2f;
            vec.Y = 0.0f;
            vec.Z = 0.0f;
            vertices.Add(vec);
            //8
            vec.X = -0.1f;
            vec.Y = 0.0f;
            vec.Z = 0.1f;
            vertices.Add(vec);
            //9
            vec.X = 0.0f;
            vec.Y = 0.3f;
            vec.Z = 0.0f;
            vertices.Add(vec);
            //10
            vec.X = 0.0f;
            vec.Y = -0.4f;
            vec.Z = 0.0f;
            vertices.Add(vec);

            indices = new List<uint>
            {
                1,2,9,
                2,3,9,
                3,4,9,
                4,5,9,
                5,6,9,
                6,7,9,
                7,8,9,
                8,1,9,
                1,2,10,
                2,3,10,
                3,4,10,
                4,5,10,
                5,6,10,
                6,7,10,
                7,8,10,
                8,1,10
            };
        }
        public void createTombak(float centerX, float centerZ, float y)
        {
            Vector3 vec;
            vertices = new List<Vector3>();
            for (int i = 0; i < 360; i++)
            {
                double degInRad = i * Math.PI / 180;
                vec.X = 0.03f * (float)Math.Cos(degInRad) + centerX;
                vec.Y = y + 1.0f;
                vec.Z = 0.03f * (float)Math.Sin(degInRad) + centerZ;
                vertices.Add(vec);
            }
            for (int i = 0; i < 360; i++)
            {
                double degInRad = i * Math.PI / 180;
                vec.X = 0.04f * (float)Math.Cos(degInRad) + centerX;
                vec.Y = y - 0.98f;
                vec.Z = 0.04f * (float)Math.Sin(degInRad) + centerZ;
                vertices.Add(vec);
            }
            for (int i = 0; i < 360; i++)
            {
                double degInRad = i * Math.PI / 180;
                vec.X = 0.08f * (float)Math.Cos(degInRad) + centerX;
                vec.Y = y + 1.0f;
                vec.Z = 0.08f * (float)Math.Sin(degInRad) + centerZ;
                vertices.Add(vec);
            }
            vec.X = centerX;
            vec.Y = y + 1.4f;
            vec.Z = centerZ;
            vertices.Add(vec);
            for (uint i = 0; i < 359; i++)
            {
                indices.Add(i);
                indices.Add(i + 1);
                indices.Add(i + 360);
            }
            for (uint i = 0; i < 359; i++)
            {
                indices.Add(i + 1);
                indices.Add(i + 360);
                indices.Add(i + 360 + 1);
            }
            for (uint i = 0; i < 359; i++)
            {
                indices.Add(i + 720);
                indices.Add(i + 720 + 1);
                indices.Add(1080);
            }
        }

        public void createTongkat(float centerX, float centerZ, float y)
        {
            Vector3 vec;
            vertices = new List<Vector3>();
            for (int i = 0; i < 360; i++)
            {
                double degInRad = i * Math.PI / 180;
                vec.X = 0.04f * (float)Math.Cos(degInRad) + centerX;
                vec.Y = y;
                vec.Z = 0.04f * (float)Math.Sin(degInRad) + centerZ;
                vertices.Add(vec);
            }
            for (int i = 0; i < 360; i++)
            {
                double degInRad = i * Math.PI / 180;
                vec.X = 0.04f * (float)Math.Cos(degInRad) + centerX;
                vec.Y = y - 2.7f;
                vec.Z = 0.04f * (float)Math.Sin(degInRad) + centerZ;
                vertices.Add(vec);
            }
            for (uint i = 0; i < 359; i++)
            {
                indices.Add(i);
                indices.Add(i + 1);
                indices.Add(i + 360);
            }
            for (uint i = 0; i < 359; i++)
            {
                indices.Add(i + 1);
                indices.Add(i + 360);
                indices.Add(i + 360 + 1);
            }
        }
        public void createMata(float x, float y, float z, float tebal, float ext)
        {
            Vector3 temp_vector;
            float _pi = (float)Math.PI;

            for (float v = -tebal / 3; v <= tebal; v += 0.0001f)
            {
                Vector3 p = setBezier(x, y, z, tebal, ext);
                for (float u = -_pi; u <= 0; u += _pi / 30)
                {

                    temp_vector.X = (p.X + (float)Math.Cos(u)) / 9f;
                    temp_vector.Y = (p.Y + (float)Math.Sin(u)) / 6;
                    temp_vector.Z = z + v / 2 + 0.5f;


                    vertices.Add(temp_vector);
                }
            }
        }

        public void createLatar(float centerX, float centerY, float centerZ , float radius)
        {
            Vector3 vec;
            vertices = new List<Vector3>();
            for (int i = 0; i < 360; i++)
            {
                double degInRad = i * Math.PI / 180;
                vec.X = radius * (float)Math.Cos(degInRad) + centerX;
                vec.Y = centerY;
                vec.Z = radius * (float)Math.Sin(degInRad) + centerZ;
                vertices.Add(vec);
            }
            vec.X = centerX;
            vec.Y = centerY;
            vec.Z = centerZ;
            vertices.Add(vec);
            for(uint i = 0;i < 360; i++)
            {
                indices.Add(i);
                indices.Add(i + 1);
                indices.Add(360);
            }
            indices.Add(359);
            indices.Add(0);
            indices.Add(360);
        }
        public void createMataBesar(float x, float y, float z, float tebal, float ext)
        {
            Vector3 temp_vector;
            float _pi = (float)Math.PI;

            for (float v = -tebal / 3; v <= tebal / 3; v += 0.0001f)
            {
                Vector3 p = setBezier(x, y, z, tebal, ext);
                for (float u = -_pi; u <= 0; u += _pi / 30)
                {

                    temp_vector.X = (p.X + (float)Math.Cos(u)) / 6f;
                    temp_vector.Y = (p.Y + (float)Math.Sin(u)) / 6;
                    temp_vector.Z = z + v / 2 + 0.5f;


                    vertices.Add(temp_vector);
                }
            }
        }
        Vector3 setBezier(float t, float x, float y, float height, float ext)
        {
            Vector3 p = new Vector3(0f, 0f, 0f);

            float[] k = new float[3];

            k[0] = (float)Math.Pow(1 - t, 3 - 1 - 0) * (float)Math.Pow(t, 0) * 1;
            k[1] = (float)Math.Pow(1 - t, 3 - 1 - 1) * (float)Math.Pow(t, 1) * 2;
            k[2] = (float)Math.Pow(1 - t, 3 - 1 - 2) * (float)Math.Pow(t, 2) * 1;


            // Titik 1
            p.X += k[0] * x;
            p.Y += k[0] * y - height;

            // Titik 2
            p.X += k[1] * (x + ext);
            p.Y += k[1] * y;

            // Titik 3
            p.X += k[2] * x;
            p.Y += k[2] * y + height;

            return p;
        }

        public void createEllipsoid(float x, float y, float z, float radX, float radY, float radZ, float sectorCount, float stackCount)
        {
            objectCenter = new Vector3(x, y, z);

            float pi = (float)Math.PI;
            Vector3 temp_vector;
            float sectorStep = 2 * pi / sectorCount;
            float stackStep = pi / stackCount;
            float sectorAngle, stackAngle, tempX, tempY, tempZ;

            for (int i = 0; i <= stackCount; ++i)
            {
                stackAngle = pi / 1.06f - i * stackStep;
                tempX = radX * (float)Math.Cos(stackAngle);
                tempY = radY * (float)Math.Sin(stackAngle);
                tempZ = radZ * (float)Math.Cos(stackAngle);

                for (int j = 0; j <= sectorCount; ++j)
                {
                    sectorAngle = j * sectorStep;

                    temp_vector.X = x + tempX * (float)Math.Cos(sectorAngle);
                    temp_vector.Y = y + tempY;
                    temp_vector.Z = z + tempZ * (float)Math.Sin(sectorAngle);

                    vertices.Add(temp_vector);
                }
            }

            uint k1, k2;
            for (int i = 0; i < stackCount; ++i)
            {
                k1 = (uint)(i * (sectorCount + 1));
                k2 = (uint)(k1 + sectorCount + 1);

                for (int j = 0; j < sectorCount; ++j, ++k1, ++k2)
                {
                    if (i != 0)
                    {
                        indices.Add(k1);
                        indices.Add(k2);
                        indices.Add(k1 + 1);

                    }

                    if (i != stackCount - 1)
                    {
                        indices.Add(k1 + 1);
                        indices.Add(k2);
                        indices.Add(k2 + 1);
                    }
                }
            }
        }

        public void createTabungTT(float centerX, float centerZ, float radius, float y)
        {
            Vector3 vec;
            vertices = new List<Vector3>();
            for (int i = 0; i < 360; i++)
            {
                double degInRad = i * Math.PI / 180;
                vec.X = radius * (float)Math.Cos(degInRad) + centerX;
                vec.Y = y;
                vec.Z = radius * (float)Math.Sin(degInRad) + centerZ;
                vertices.Add(vec);
            }
            for (int i = 0; i < 360; i++)
            {
                double degInRad = i * Math.PI / 180;
                vec.X = radius * (float)Math.Cos(degInRad) + centerX;
                vec.Y = y - 0.3f;
                vec.Z = radius * (float)Math.Sin(degInRad) + centerZ;
                vertices.Add(vec);
            }
            for (uint i = 0; i < 359; i++)
            {
                indices.Add(i);
                indices.Add(i + 1);
                indices.Add(i + 360);
            }
            for (uint i = 0; i < 359; i++)
            {
                indices.Add(i + 1);
                indices.Add(i + 360);
                indices.Add(i + 360 + 1);
            }
        }

        #region transforms
        public void rotate(Vector3 pivot, Vector3 vector, float angle)
        {
            var radAngle = MathHelper.DegreesToRadians(angle);

            var arbRotationMatrix = new Matrix4
                (
                new Vector4((float)(Math.Cos(radAngle) + Math.Pow(vector.X, 2.0f) * (1.0f - Math.Cos(radAngle))), (float)(vector.X * vector.Y * (1.0f - Math.Cos(radAngle)) + vector.Z * Math.Sin(radAngle)), (float)(vector.X * vector.Z * (1.0f - Math.Cos(radAngle)) - vector.Y * Math.Sin(radAngle)), 0),
                new Vector4((float)(vector.X * vector.Y * (1.0f - Math.Cos(radAngle)) - vector.Z * Math.Sin(radAngle)), (float)(Math.Cos(radAngle) + Math.Pow(vector.Y, 2.0f) * (1.0f - Math.Cos(radAngle))), (float)(vector.Y * vector.Z * (1.0f - Math.Cos(radAngle)) + vector.X * Math.Sin(radAngle)), 0),
                new Vector4((float)(vector.X * vector.Z * (1.0f - Math.Cos(radAngle)) + vector.Y * Math.Sin(radAngle)), (float)(vector.Y * vector.Z * (1.0f - Math.Cos(radAngle)) - vector.X * Math.Sin(radAngle)), (float)(Math.Cos(radAngle) + Math.Pow(vector.Z, 2.0f) * (1.0f - Math.Cos(radAngle))), 0),
                Vector4.UnitW
                );

            model *= Matrix4.CreateTranslation(-pivot);
            model *= arbRotationMatrix;
            model *= Matrix4.CreateTranslation(pivot);

            for (int i = 0; i < 3; i++)
            {
                _euler[i] = Vector3.Normalize(getRotationResult(pivot, vector, radAngle, _euler[i], true));
            }

            objectCenter = getRotationResult(pivot, vector, radAngle, objectCenter);

            foreach (var i in child)
            {
                i.rotate(pivot, vector, angle);
            }
        }

        public Vector3 getRotationResult(Vector3 pivot, Vector3 vector, float angle, Vector3 point, bool isEuler = false)
        {
            Vector3 temp, newPosition;

            if (isEuler)
            {
                temp = point;
            }
            else
            {
                temp = point - pivot;
            }

            newPosition.X =
                temp.X * (float)(Math.Cos(angle) + Math.Pow(vector.X, 2.0f) * (1.0f - Math.Cos(angle))) +
                temp.Y * (float)(vector.X * vector.Y * (1.0f - Math.Cos(angle)) - vector.Z * Math.Sin(angle)) +
                temp.Z * (float)(vector.X * vector.Z * (1.0f - Math.Cos(angle)) + vector.Y * Math.Sin(angle));

            newPosition.Y =
                temp.X * (float)(vector.X * vector.Y * (1.0f - Math.Cos(angle)) + vector.Z * Math.Sin(angle)) +
                temp.Y * (float)(Math.Cos(angle) + Math.Pow(vector.Y, 2.0f) * (1.0f - Math.Cos(angle))) +
                temp.Z * (float)(vector.Y * vector.Z * (1.0f - Math.Cos(angle)) - vector.X * Math.Sin(angle));

            newPosition.Z =
                temp.X * (float)(vector.X * vector.Z * (1.0f - Math.Cos(angle)) - vector.Y * Math.Sin(angle)) +
                temp.Y * (float)(vector.Y * vector.Z * (1.0f - Math.Cos(angle)) + vector.X * Math.Sin(angle)) +
                temp.Z * (float)(Math.Cos(angle) + Math.Pow(vector.Z, 2.0f) * (1.0f - Math.Cos(angle)));

            if (isEuler)
            {
                temp = newPosition;
            }
            else
            {
                temp = newPosition + pivot;
            }
            return temp;
        }

        public void translate(float x, float y, float z)
        {
            model *= Matrix4.CreateTranslation(x, y, z);
            objectCenter.X += x;
            objectCenter.Y += y;
            objectCenter.Z += z;

            foreach (var i in child)
            {
                i.translate(x, y, z);
            }
        }

        public void scale(float scaleX, float scaleY, float scaleZ)
        {
            model *= Matrix4.CreateTranslation(-objectCenter);
            model *= Matrix4.CreateScale(scaleX, scaleY, scaleZ);
            model *= Matrix4.CreateTranslation(objectCenter);

            foreach (var i in child)
            {
                i.scale(scaleX, scaleY, scaleZ);
            }
        }
        #endregion
    }
}
