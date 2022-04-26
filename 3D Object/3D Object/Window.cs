using LearnOpenTK.Common;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;
using OpenTK.Windowing.GraphicsLibraryFramework;

namespace _3D_Object
{
    class Window : GameWindow
    {
        List<Asset3d> slimeAir2 = new List<Asset3d>();
        List<Asset3d> slimeSpace2 = new List<Asset3d>();
        List<Asset3d> diamond2 = new List<Asset3d>();
        List<Asset3d> bamboo = new List<Asset3d>();
        List<Asset3d> bamboo1 = new List<Asset3d>();
        List<Asset3d> bamboo2 = new List<Asset3d>();
        List<Asset3d> bamboo3 = new List<Asset3d>();
        List<Asset3d> bamboo4 = new List<Asset3d>();
        List<Asset3d> bamboo5 = new List<Asset3d>();
        List<Asset3d> bamboo6 = new List<Asset3d>();
        List<Asset3d> bamboo7 = new List<Asset3d>();
        List<Asset3d> bamboo8 = new List<Asset3d>();
        List<Asset3d> bamboo9 = new List<Asset3d>();
        List<Asset3d> bamboo10 = new List<Asset3d>();
        List<Asset3d> bamboo11 = new List<Asset3d>();
        List<Asset3d> bamboo12 = new List<Asset3d>();
        List<Asset3d> bamboo13 = new List<Asset3d>();
        List<Asset3d> bamboo14 = new List<Asset3d>();
        List<Asset3d> bamboo15 = new List<Asset3d>();
        List<Asset3d> latars = new List<Asset3d>();
        Camera _camera;
        bool _firstMove = true;
        Vector2 _lastPos;
        Vector3 _objectPos = new Vector3(0,0,0);
        float _rotationSpeed = 1.0f;
        public Window(GameWindowSettings gameWindowSettings, NativeWindowSettings nativeWindowSettings) : base(gameWindowSettings, nativeWindowSettings)
        {

        }

        protected override void OnLoad()
        {
            base.OnLoad();

            GL.ClearColor(0.67f, 0.72f, 0.57f, 1.0f);
            GL.Enable(EnableCap.DepthTest);

            _camera = new Camera(new Vector3(0.0f, 0.0f, 4.0f), Size.X / Size.Y);
            CursorGrabbed = false;
            _camera.AspectRatio = Size.X / (float)Size.Y;
            //3D
            var slimeAir = new Asset3d(new Vector3(0.6f, 0.996f, 1.0f));
            slimeAir.createEllipsoid(-1.0f, -2.0f, 1.0f, 1.0f, 1.0f, 1.0f, 400.0f, 400.0f);
            slimeAir2.Add(slimeAir);

            var topiAir = new Asset3d(new Vector3(0.56f, 0.87f, 0.93f));
               topiAir.createTopi2(-1.0f, 1.0f, 1.0f);
            slimeAir2.Add(topiAir);

           /* var aura = new Asset3d(new Vector3(0.83f, 0.5f, 0.99f));
            aura.createAura(-1.0f, 1.0f, 1.4f);
            slimeAir2.Add(aura);*/

            var slimeSpace = new Asset3d(new Vector3(0.44f, 0.07f, 0.49f));
            slimeSpace.createEllipsoid(1.0f, 0.5f, 0.5f, 0.7f, 0.7f, 0.7f, 400.0f, 400.0f);
            slimeSpace2.Add(slimeSpace);

            var portal = new Asset3d(new Vector3(0.6f, 0.0f, 0.94f));
            portal.createPortal(1.0f, 0.5f, 0.5f);
            slimeSpace2.Add(portal);

            /*var beam = new Asset3d(new Vector3(0.72f, 0.51f, 1f));
            beam.createBeam(1.0f, 0.5f, 0.6f);
            slimeSpace2.Add(beam);*/

            var tanduk1 = new Asset3d(new Vector3(0.44f, 0.07f, 0.49f));
            tanduk1.createTanduk(0.5f, 0.5f, 0.2f);
            slimeSpace2.Add(tanduk1);

            var tanduk2 = new Asset3d(new Vector3(0.44f, 0.07f, 0.49f));
            tanduk2.createTanduk(1.5f, 0.5f, 0.2f);
            slimeSpace2.Add(tanduk2);

            var eye1 = new Asset3d(new Vector3((0.72f, 0.51f, 1f)));
            eye1.createMata(0.05f, 0.4f, -0.3f, 0.001f, 0.5f);
            slimeSpace2.Add(eye1);
            eye1.translate(1.3f, 0.7f, -0.35f);

            var eye2 = new Asset3d(new Vector3((0.72f, 0.51f, 1f)));
            eye2.createMata(0.05f, 0.4f, -0.3f, 0.001f, 0.5f);
            slimeSpace2.Add(eye2);
            eye2.translate(0.6f, 0.7f, -0.35f);

            var eye3 = new Asset3d(new Vector3((0.72f, 0.51f, 1f)));
            eye3.createMataBesar(-1.0f, 0.4f, -0.3f, 0.001f, 0.5f);
            slimeAir2.Add(eye3);
            eye3.translate(-0.3f, -1.7f, 1.75f);

            var eye4 = new Asset3d(new Vector3((0.72f, 0.51f, 1f)));
            eye4.createMataBesar(-1.0f, 0.4f, -0.3f, 0.001f, 0.5f);
            slimeAir2.Add(eye4);
            eye4.translate(-1.2f, -1.7f, 1.75f);

            var tombak = new Asset3d(new Vector3((0.6f, 0f, 0f)));
            tombak.createTombak(1.7f, 0.5f, 1.35f);
            slimeSpace2.Add(tombak);

            var tongkat = new Asset3d(new Vector3((0.08f, 0.44f, 0.63f)));
            tongkat.createTongkat(-2.0f, 1.0f, 0.5f);
            slimeAir2.Add(tongkat);

            var diamond = new Asset3d(new Vector3(0.98f, 0.97f, 0.46f));
            diamond.createDiamond(2.0f, 1.0f, -3.0f);
            diamond2.Add(diamond);

            var latar = new Asset3d(new Vector3(0.32f, 0.22f, 0.02f));
            latar.createLatar(0.0f, -2.5f, -3.0f,6.0f);
            latars.Add(latar);

            //kanan
            bamboo = createBamboo(bamboo, 1.0f, -3.0f);
            bamboo1 = createBamboo2(bamboo1, 1.3f, -3.0f);
            bamboo2 = createBamboo(bamboo2, 1.6f, -3.0f);
            bamboo3 = createBamboo2(bamboo3, 1.9f, -3.0f);
            bamboo4 = createBamboo(bamboo4, 2.2f, -3.0f);
            bamboo5 = createBamboo2(bamboo5, 2.5f, -3.0f);
            bamboo6 = createBamboo(bamboo6, 2.8f, -3.0f);
            bamboo7 = createBamboo2(bamboo7, 3.1f, -3.0f);
            //kiri
            bamboo8 = createBamboo(bamboo8, -1.0f, -3.0f);
            bamboo9 = createBamboo2(bamboo9, -1.3f, -3.0f);
            bamboo10 = createBamboo(bamboo10, -1.6f, -3.0f);
            bamboo11 = createBamboo2(bamboo11, -1.9f, -3.0f);
            bamboo12 = createBamboo(bamboo12, -2.2f, -3.0f);
            bamboo13 = createBamboo2(bamboo13, -2.5f, -3.0f);
            bamboo14 = createBamboo(bamboo14, -2.8f, -3.0f);
            bamboo15 = createBamboo2(bamboo15, -3.1f, -3.0f);

            foreach (Asset3d i in slimeAir2)
            {
                i.load(Size.X, Size.Y);
            }
            foreach (Asset3d i in slimeSpace2)
            {
                i.load(Size.X, Size.Y);
            }
            foreach (Asset3d i in diamond2)
            {
                i.load(Size.X, Size.Y);
            }
            foreach (Asset3d i in bamboo)
            {
                i.load(Size.X, Size.Y);
            }
            foreach (Asset3d i in bamboo1)
            {
                i.load(Size.X, Size.Y);
            }
            foreach (Asset3d i in bamboo2)
            {
                i.load(Size.X, Size.Y);
            }
            foreach (Asset3d i in bamboo3)
            {
                i.load(Size.X, Size.Y);
            }
            foreach (Asset3d i in bamboo4)
            {
                i.load(Size.X, Size.Y);
            }
            foreach (Asset3d i in bamboo5)
            {
                i.load(Size.X, Size.Y);
            }
            foreach (Asset3d i in bamboo6)
            {
                i.load(Size.X, Size.Y);
            }
            foreach (Asset3d i in bamboo7)
            {
                i.load(Size.X, Size.Y);
            }
            foreach (Asset3d i in bamboo8)
            {
                i.load(Size.X, Size.Y);
            }
            foreach (Asset3d i in bamboo9)
            {
                i.load(Size.X, Size.Y);
            }
            foreach (Asset3d i in bamboo10)
            {
                i.load(Size.X, Size.Y);
            }
            foreach (Asset3d i in bamboo11)
            {
                i.load(Size.X, Size.Y);
            }
            foreach (Asset3d i in bamboo12)
            {
                i.load(Size.X, Size.Y);
            }
            foreach (Asset3d i in bamboo13)
            {
                i.load(Size.X, Size.Y);
            }
            foreach (Asset3d i in bamboo14)
            {
                i.load(Size.X, Size.Y);
            }
            foreach (Asset3d i in bamboo15)
            {
                i.load(Size.X, Size.Y);
            }
            latars[0].load(Size.X, Size.Y);
        }

        protected override void OnRenderFrame(FrameEventArgs args)
        {
            base.OnRenderFrame(args);

            float time = (float)args.Time;

            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
            foreach (Asset3d i in slimeSpace2)
            {
                i.render(_camera.GetViewMatrix(), _camera.GetProjectionMatrix(), 1);
                var w = 0;
                for (w = 0; w <= 5; w++)
                {
                    i.rotate(Vector3.Zero, Vector3.UnitY, 10 * time);
                }
            }
            foreach (Asset3d i in slimeAir2)
            {
                i.render(_camera.GetViewMatrix(), _camera.GetProjectionMatrix(), 4);
                i.rotate(Vector3.Zero, Vector3.UnitY, 20 * time);
            }
            foreach (Asset3d i in diamond2)
            {
                i.render(_camera.GetViewMatrix(), _camera.GetProjectionMatrix(), 0);
                i.rotate(Vector3.Zero, Vector3.UnitY, 10 * time);
            }
            foreach (Asset3d i in bamboo)
            {
                i.render(_camera.GetViewMatrix(), _camera.GetProjectionMatrix(), 2);
            }
            foreach (Asset3d i in bamboo1)
            {
                i.render(_camera.GetViewMatrix(), _camera.GetProjectionMatrix(), 3);
            }
            foreach (Asset3d i in bamboo2)
            {
                i.render(_camera.GetViewMatrix(), _camera.GetProjectionMatrix(), 2);
            }
            foreach (Asset3d i in bamboo3)
            {
                i.render(_camera.GetViewMatrix(), _camera.GetProjectionMatrix(), 3);
            }
            foreach (Asset3d i in bamboo4)
            {
                i.render(_camera.GetViewMatrix(), _camera.GetProjectionMatrix(), 2);
            }
            foreach (Asset3d i in bamboo5)
            {
                i.render(_camera.GetViewMatrix(), _camera.GetProjectionMatrix(), 3);
            }
            foreach (Asset3d i in bamboo6)
            {
                i.render(_camera.GetViewMatrix(), _camera.GetProjectionMatrix(), 2);
            }

            foreach (Asset3d i in bamboo7)
            {
                i.render(_camera.GetViewMatrix(), _camera.GetProjectionMatrix(), 3);
            }
            foreach (Asset3d i in bamboo8)
            {
                i.render(_camera.GetViewMatrix(), _camera.GetProjectionMatrix(), 2);
            }
            foreach (Asset3d i in bamboo9)
            {
                i.render(_camera.GetViewMatrix(), _camera.GetProjectionMatrix(), 3);
            }
            foreach (Asset3d i in bamboo10)
            {
                i.render(_camera.GetViewMatrix(), _camera.GetProjectionMatrix(), 2);
            }
            foreach (Asset3d i in bamboo11)
            {
                i.render(_camera.GetViewMatrix(), _camera.GetProjectionMatrix(), 3);
            }
            foreach (Asset3d i in bamboo12)
            {
                i.render(_camera.GetViewMatrix(), _camera.GetProjectionMatrix(), 2);
            }
            foreach (Asset3d i in bamboo13)
            {
                i.render(_camera.GetViewMatrix(), _camera.GetProjectionMatrix(), 3);
            }
            foreach (Asset3d i in bamboo14)
            {
                i.render(_camera.GetViewMatrix(), _camera.GetProjectionMatrix(), 2);
            }
            foreach (Asset3d i in bamboo15)
            {
                i.render(_camera.GetViewMatrix(), _camera.GetProjectionMatrix(), 3);
            }
            latars[0].render(_camera.GetViewMatrix(), _camera.GetProjectionMatrix(), 0);
            SwapBuffers();
        }
        public Matrix4 generateArbRotationMatrix(Vector3 axis, Vector3 center, float degree)
        {
            var rads = MathHelper.DegreesToRadians(degree);

            var secretFormula = new float[4, 4] {
                { (float)Math.Cos(rads) + (float)Math.Pow(axis.X, 2) * (1 - (float)Math.Cos(rads)), axis.X* axis.Y * (1 - (float)Math.Cos(rads)) - axis.Z * (float)Math.Sin(rads),    axis.X * axis.Z * (1 - (float)Math.Cos(rads)) + axis.Y * (float)Math.Sin(rads),   0 },
                { axis.Y * axis.X * (1 - (float)Math.Cos(rads)) + axis.Z * (float)Math.Sin(rads),   (float)Math.Cos(rads) + (float)Math.Pow(axis.Y, 2) * (1 - (float)Math.Cos(rads)), axis.Y * axis.Z * (1 - (float)Math.Cos(rads)) - axis.X * (float)Math.Sin(rads),   0 },
                { axis.Z * axis.X * (1 - (float)Math.Cos(rads)) - axis.Y * (float)Math.Sin(rads),   axis.Z * axis.Y * (1 - (float)Math.Cos(rads)) + axis.X * (float)Math.Sin(rads),   (float)Math.Cos(rads) + (float)Math.Pow(axis.Z, 2) * (1 - (float)Math.Cos(rads)), 0 },
                { 0, 0, 0, 1}
            };
            var secretFormulaMatix = new Matrix4
            (
                new Vector4(secretFormula[0, 0], secretFormula[0, 1], secretFormula[0, 2], secretFormula[0, 3]),
                new Vector4(secretFormula[1, 0], secretFormula[1, 1], secretFormula[1, 2], secretFormula[1, 3]),
                new Vector4(secretFormula[2, 0], secretFormula[2, 1], secretFormula[2, 2], secretFormula[2, 3]),
                new Vector4(secretFormula[3, 0], secretFormula[3, 1], secretFormula[3, 2], secretFormula[3, 3])
            );

            return secretFormulaMatix;
        }

        protected override void OnUpdateFrame(FrameEventArgs args)
        {
            base.OnUpdateFrame(args);

            float time = (float)args.Time;

            if (!IsFocused)
            {
                return;
            }

            var input = KeyboardState;
            float cameraSpeed = 1.0f;
            if (input.IsKeyDown(Keys.Escape))
            {
                Close();
            }
            if (KeyboardState.IsKeyDown(Keys.S))
            {
                _camera.Position -= _camera.Front * cameraSpeed * (float)args.Time;

            }
            if (KeyboardState.IsKeyDown(Keys.W))
            {
                _camera.Position += _camera.Front * cameraSpeed * (float)args.Time;

            }
            if (KeyboardState.IsKeyDown(Keys.A))
            {
                _camera.Position -= _camera.Right * cameraSpeed * (float)args.Time;

            }
            if (KeyboardState.IsKeyDown(Keys.D))
            {
                _camera.Position += _camera.Right * cameraSpeed * (float)args.Time;

            }
            if (KeyboardState.IsKeyDown(Keys.LeftShift))
            {
                _camera.Position -= _camera.Up * cameraSpeed * (float)args.Time;

            }
            if (KeyboardState.IsKeyDown(Keys.X))
            {
                _camera.Position += _camera.Up * cameraSpeed * (float)args.Time;

            }
            var mouse = MouseState;
            var sensitivity = 0.02f;
            if (_firstMove)
            {
                _lastPos = new Vector2(mouse.X, mouse.Y);
                _firstMove = false;
            }
            else
            {
                var deltaX = mouse.X - _lastPos.X;
                var deltaY = mouse.Y - _lastPos.Y;
                _lastPos = new Vector2(mouse.X,mouse.Y);
                _camera.Yaw += deltaX * sensitivity;
                _camera.Pitch -= deltaY * sensitivity;
            }
            if(KeyboardState.IsKeyDown(Keys.N))
            {
                var axis = new Vector3(0, 1, 0);
                _camera.Position -= _objectPos;
                _camera.Position = Vector3.Transform(
                    _camera.Position,
                    generateArbRotationMatrix(axis, _objectPos, _rotationSpeed)
                    .ExtractRotation());
                _camera.Position += _objectPos;
                _camera._front = -Vector3.Normalize(_camera.Position
                    - _objectPos);
            }
            if (KeyboardState.IsKeyDown(Keys.Comma))
            {
                var axis = new Vector3(0, 1, 0);
                _camera.Position -= _objectPos;
                _camera.Yaw -= _rotationSpeed;
                _camera.Position = Vector3.Transform(_camera.Position,
                    generateArbRotationMatrix(axis, _objectPos, -_rotationSpeed)
                    .ExtractRotation());
                _camera.Position += _objectPos;

                _camera._front = -Vector3.Normalize(_camera.Position - _objectPos);
            }
            if (KeyboardState.IsKeyDown(Keys.K))
            {
                var axis = new Vector3(1, 0, 0);
                _camera.Position -= _objectPos;
                _camera.Pitch -= _rotationSpeed;
                _camera.Position = Vector3.Transform(_camera.Position,
                    generateArbRotationMatrix(axis, _objectPos, _rotationSpeed).ExtractRotation());
                _camera.Position += _objectPos;
                _camera._front = -Vector3.Normalize(_camera.Position - _objectPos);
            }
            if (KeyboardState.IsKeyDown(Keys.M))
            {
                var axis = new Vector3(1, 0, 0);
                _camera.Position -= _objectPos;
                _camera.Pitch += _rotationSpeed;
                _camera.Position = Vector3.Transform(_camera.Position,
                    generateArbRotationMatrix(axis, _objectPos, -_rotationSpeed).ExtractRotation());
                _camera.Position += _objectPos;
                _camera._front = -Vector3.Normalize(_camera.Position - _objectPos);
            }
        }

        protected override void OnResize(ResizeEventArgs e)
        {
            base.OnResize(e);

            GL.Viewport(0, 0, Size.X, Size.Y);
        }
        public List<Asset3d> createBamboo(List<Asset3d> balingbalingbamboo, float x, float z)
        {
            for (float i = -2.0f; i < 0.6f; i += 0.3f)
            {
                Asset3d bambu1 = new Asset3d(new Vector3(0.19f, 0.31f, 0.18f));
                bambu1.createTabungTT(x, z, 0.1f, i);
                balingbalingbamboo.Add(bambu1);
                i += 0.3f;
                Asset3d bambu2 = new Asset3d(new Vector3(0.25f, 0.39f, 0.26f));
                bambu2.createTabungTT(x, z, 0.1f, i);
                balingbalingbamboo.Add(bambu2);

            }
            return balingbalingbamboo;
        }
        public List<Asset3d> createBamboo2(List<Asset3d> balingbalingbamboo, float x, float z)
        {
            for (float i = -2.0f; i < 0.6f; i += 0.3f)
            {
                Asset3d bambu1 = new Asset3d(new Vector3(0.54f, 0.65f, 0.27f));
                bambu1.createTabungTT(x, z, 0.1f, i);
                balingbalingbamboo.Add(bambu1);
                i += 0.3f;
                Asset3d bambu2 = new Asset3d(new Vector3(0.47f, 0.57f, 0.31f));
                bambu2.createTabungTT(x, z, 0.1f, i);
                balingbalingbamboo.Add(bambu2);

            }
            return balingbalingbamboo;
        }

        protected override void OnMouseWheel(MouseWheelEventArgs e)
        {
            base.OnMouseWheel(e);
            _camera.Fov = _camera.Fov - e.Offset.Y;
        }

    }
}
