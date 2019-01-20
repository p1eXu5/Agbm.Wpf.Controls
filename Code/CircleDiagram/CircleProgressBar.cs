using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace CircleDiagram
{
    /// <summary>
    /// Follow steps 1a or 1b and then 2 to use this custom control in a XAML file.
    ///
    /// Step 1a) Using this custom control in a XAML file that exists in the current project.
    /// Add this XmlNamespace attribute to the root element of the markup file where it is 
    /// to be used:
    ///
    ///     xmlns:MyNamespace="clr-namespace:CircleDiagram"
    ///
    ///
    /// Step 1b) Using this custom control in a XAML file that exists in a different project.
    /// Add this XmlNamespace attribute to the root element of the markup file where it is 
    /// to be used:
    ///
    ///     xmlns:MyNamespace="clr-namespace:CircleDiagram;assembly=CircleDiagram"
    ///
    /// You will also need to add a project reference from the project where the XAML file lives
    /// to this project and Rebuild to avoid compilation errors:
    ///
    ///     Right click on the target project in the Solution Explorer and
    ///     "Add Reference"->"Projects"->[Browse to and select this project]
    ///
    ///
    /// Step 2)
    /// Go ahead and use your control in the XAML file.
    ///
    ///     <MyNamespace:CircleProgressBar/>
    ///
    /// </summary>
    public class CircleProgressBar : RangeBase
    {
        private const double MAX_START_POSITION = 360.0d;

        static CircleProgressBar ()
        {
            DefaultStyleKeyProperty.OverrideMetadata( typeof( CircleProgressBar ), new FrameworkPropertyMetadata( typeof( CircleProgressBar ) ) );

            MaximumProperty.OverrideMetadata( typeof( CircleProgressBar ), new FrameworkPropertyMetadata( MAX_START_POSITION ) );
            
        }

        public static DependencyProperty SweepDirectionProperty = ArcSegment.SweepDirectionProperty.AddOwner( typeof( CircleProgressBar ), new FrameworkPropertyMetadata( SweepDirection.Clockwise ) );

        public SweepDirection SweepDirection
        {
            get => ( SweepDirection )GetValue( ArcSegment.SweepDirectionProperty );
            set => SetValue( ArcSegment.SweepDirectionProperty, value );
        }

        public double StartPosition
        {
            get { return ( double )GetValue( StartPositionProperty ); }
            set { SetValue( StartPositionProperty, value ); }
        }

        // Using a DependencyProperty as the backing store for StartPosition.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty StartPositionProperty =
            DependencyProperty.Register( "StartPosition", typeof( double ), typeof( CircleProgressBar ), 
                                        new FrameworkPropertyMetadata( 0.0d, null, new CoerceValueCallback( CoerceStartPosition ) ), 
                                        new ValidateValueCallback( IsValidDoubleValue ) );

        private static object CoerceStartPosition ( DependencyObject d, object value )
        {
            CircleProgressBar ctrl = ( CircleProgressBar )d;
            double startPosition = ctrl.StartPosition;

            if ( startPosition < 0.0d ) return 0.0d;
            if ( startPosition > MAX_START_POSITION ) return MAX_START_POSITION;

            return value;
        }

        private static bool IsValidDoubleValue ( object value )
        {
            double val = ( double )value;
            return !double.IsInfinity( val );
        }
    }
}
