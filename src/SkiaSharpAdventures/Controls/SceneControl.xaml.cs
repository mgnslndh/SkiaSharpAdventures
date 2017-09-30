using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms.Integration;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using SkiaSharp;
using SkiaSharp.Views.Desktop;

namespace SkiaSharpAdventures.Controls
{
    /// <summary>
    /// Interaction logic for SceneControl.xaml
    /// </summary>
    public partial class SceneControl : UserControl
    {
        public SceneControl()
        {
            InitializeComponent();
            RenderBackend = RenderBackend.SKElement;            
        }

        private void SkiaHost_OnPaintSurface(object sender, SKPaintSurfaceEventArgs e)
        {
            OnPaintCanvas(sender, e);
        }

        private void OpenGlHost_OnInitialized(object sender, EventArgs e)
        {
            var glControl = new SKGLControl();
            glControl.PaintSurface += OnPaintOpenGl;
            glControl.Dock = System.Windows.Forms.DockStyle.Fill;


            var host = (WindowsFormsHost) sender;
            host.Child = glControl;
        }

        private void OnPaintOpenGl(object sender, SKPaintGLSurfaceEventArgs e)
        {
            OnPaintSurface(e.Surface.Canvas, e.RenderTarget.Width, e.RenderTarget.Height);
        }

        private void OnPaintCanvas(object sender, SKPaintSurfaceEventArgs e)
        {
            OnPaintSurface(e.Surface.Canvas, e.Info.Width, e.Info.Height);
        }

        private void OnPaintSurface(SKCanvas canvas, int width, int height)
        {
        }

        private RenderBackend _renderBackend;
        public RenderBackend RenderBackend
        {
            get => _renderBackend;
            set
            {
                _renderBackend = value;
                if (_renderBackend == RenderBackend.SKElement)
                {
                    SkiaHost.Visibility = Visibility.Visible;
                    OpenGlHost.Visibility = Visibility.Collapsed;
                }
                else
                {
                    SkiaHost.Visibility = Visibility.Collapsed;
                    OpenGlHost.Visibility = Visibility.Visible;
                }
            }
        }
    }

    
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    public enum RenderBackend
    {        
        SKElement,
        SKGLControl
    }
}
