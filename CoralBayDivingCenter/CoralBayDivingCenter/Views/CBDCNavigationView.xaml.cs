using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CoralBayDivingCenter.Views
{
    public partial class CBDCNavigationView : NavigationPage
    {
        public CBDCNavigationView() : base()
        {
            InitializeComponent();
        }

        public CBDCNavigationView(Page root) : base(root)
        {
            InitializeComponent();
        }
    }
}