using StardewValley.Menus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using StardewValley;
using StardewValley.BellsAndWhistles;

namespace StardewValleyMP
{
    public delegate void EventBase(System.Object sender, EventArgs e);
    enum ClickMode
    {
        Left = 0,
        Right = 1,
        Mid = 2,
    }

    class Control
    {
        public event EventBase ClickEvent;
        public event EventBase MoveEvent;

        public Rectangle rect = new Rectangle();
        public virtual int X
        {
            get { return rect.X; }
            set { rect.X = value; }
        }
        public virtual int Y
        {
            get { return rect.Y; }
            set { rect.Y = value; }
        }

        public virtual int Width
        {
            get { return rect.Width; }
            set { rect.Width = value; }
        }

        public virtual int Height
        {
            get { return rect.Height; }
            set { rect.Height = value; }
        }

        public Point Location
        {
            get
            {
                return new Point(rect.X, rect.Y);
            }
            set
            {
                rect.X = value.X;
                rect.Y = value.Y;
            }
        }

        public virtual string Text
        {
            set; get;
        }

        public virtual void draw(SpriteBatch b) { }

        public virtual void onClick(int x, int y)
        {
            if (rect.Contains(x, y))
                ClickEvent(this, new EventArgs());
        }

        public virtual void onMouseMove(int x, int y)
        {
            if (rect.Contains(x, y))
                MoveEvent(this, new EventArgs());
        }



    }

    class UIButton : Control
    {
        bool isSelect = false;
        public override void draw(SpriteBatch b)
        {

            if (!isSelect)
            {
                isSelect = rect.Contains(Game1.getOldMouseX(), Game1.getOldMouseY());
                if (isSelect)
                    Game1.playSound("Cowboy_gunshot");
            }
            else
                isSelect = rect.Contains(Game1.getOldMouseX(), Game1.getOldMouseY());
            IClickableMenu.drawTextureBox(b, Game1.mouseCursors, new Rectangle(384, 373, 18, 18), rect.X, rect.Y, rect.Width, rect.Height, isSelect ? Color.Wheat : Color.White, (float)Game1.pixelZoom, true);
            if(Text!=null)
                SpriteText.drawString(b, Text, rect.X + rect.Width / 2 - SpriteText.getWidthOfString(Text) / 2, rect.Y + rect.Height / 2 - SpriteText.getHeightOfString(Text) / 2);
            base.draw(b);
        }
    }

    class UITextBox : Control
    {
        TextBox Handle = new TextBox(Game1.content.Load<Texture2D>("LooseSprites\\textBox"), null, Game1.smallFont, Game1.textColor);
        public override string Text
        {
            set
            {
                Handle.Text = value;
            }
            get
            {
                return Handle.Text;
            }
        }

        public override int X
        {
            get
            {
                return Handle.X;
            }

            set
            {
                Handle.X = value;
            }
        }

        public override int Y
        {
            get
            {
                return Handle.Y;
            }

            set
            {
                Handle.Y = value;
            }
        }

        public override int Width
        {
            get
            {
                return Handle.Width;
            }

            set
            {
                Handle.Width = value;
            }
        }

        public override int Height
        {
            get
            {
                return Handle.Height;
            }

            set
            {
                Handle.Height = value;
            }
        }

        public override void draw(SpriteBatch b)
        {
            Handle.Draw(b);
            base.draw(b);
        }
    }

    class UILable : Control
    {

        public UILable()
        {
            Color = Color.White;
            isSamll = true;
        }

        public bool isSamll
        {
            get; set;
        }

        public Color Color
        {
            get; set;
        }

        public override void draw(SpriteBatch b)
        {
            if(!isSamll)
                SpriteText.drawString(b, Text, rect.X, rect.Y);
            else
                Util.drawStr(Text, rect.X, rect.Y, Color);
            base.draw(b);
        }
    }

    class UICheck : Control
    {
        OptionsCheckbox Handle;
        public override void draw(SpriteBatch b)
        {
            if (Handle != null)
                Handle.draw(b,rect.X,rect.Y);
            else if(rect.Width!=0&&rect.Height!=0&&Text!=null)
            {
                Handle.bounds = rect;

            }
            base.draw(b);
        }
    }

    class UITools : IClickableMenu
    {
        public List<Control> Controls = new List<Control>();

        public override void draw(SpriteBatch b)
        {
            foreach(Control c in Controls) { c.draw(b); }
            //base.draw(b);
        }

        public override void receiveLeftClick(int x, int y, bool playSound = true)
        {
            foreach (Control c in Controls) { c.onClick(x, y); }
            //base.receiveLeftClick(x, y, false);
        }

        public override void performHoverAction(int x, int y)
        {
            base.performHoverAction(x, y);
        }

        public override void receiveRightClick(int x, int y, bool playSound = true)
        {

        }
    }
}
