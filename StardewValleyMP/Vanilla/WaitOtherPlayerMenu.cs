using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StardewValleyMP.Vanilla
{
    class WaitOtherPlayerMenu:UITools
    {
        public bool isCheck
        {
            get; set;
        }
        public WaitOtherPlayerMenu()
        {
            UIButton b = new UIButton();
            b.Text = "离开";
            b.X = base.width / 2 - 50;
            b.Y = base.height - 50;
            b.Width = 100;
            b.Height = 40;
            Controls.Add(b);
            UILable lab = new UILable();
            lab.Text = "等待其他玩家";
            lab.isSamll = false;
            lab.X = b.X;
            lab.Y = b.Y - 50;
            Controls.Add(lab);
            b.ClickEvent += B_ClickEvent;
        }

        private void B_ClickEvent(object sender, EventArgs e)
        {
            isCheck = true;
        }
    }
}
