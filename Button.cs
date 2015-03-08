using System;
using Microsoft.Xna.Framework;

namespace XF9.UI
{
	abstract class Button
    {
        /// <summary>
        /// the bounding box of the button
        /// </summary>
        protected Rectangle buttonBoundingbox;

        /// <summary>
        /// the bounding box of the mouse
        /// </summary>
        protected Rectangle mouseBoundingbox;

        /// <summary>
        /// whether the button is a toggle button or a push button
        /// </summary>
        private Boolean toggle;

        private Boolean isActive;
        /// <summary>
        /// used for toggle buttons -> true = active
        /// </summary>
        public Boolean IsActive
        {
            get { return isActive; }
            set { isActive = value; }
        }

        /// <summary>
        /// clickhandler for click events
        /// </summary>
        public event EventHandler onClick;

        /// <summary>
        /// the button color to draw with
        /// </summary>
        protected Color color;

        /// <summary>
        /// default color for the button
        /// </summary>
        private Color defaultColor = new Color(140, 140, 140);

        /// <summary>
        /// default color for the button if hovered
        /// </summary>
        private Color defaultHover = new Color(0, 133, 188);

        /// <summary>
        /// default color for the button if active
        /// </summary>
        private Color defaultActive = Microsoft.Xna.Framework.Color.LightBlue;

        /// <summary>
        /// a basic button.
        /// </summary>
        /// <param name="toggle">should the button be a toggle button?</param>
        public Button(Boolean toggle)
        {
            this.buttonBoundingbox= new Rectangle();
            this.mouseBoundingbox = new Rectangle(0, 0, 10, 10);
            this.toggle = toggle;
            this.isActive = false;
            this.color = Color.White;
        }

        /// <summary>
        /// updates the bounding box of the button
        /// </summary>
        /// <param name="buttonBoundingbox">the new bounding box</param>
        protected void UpdateBoundingbox(Rectangle buttonBoundingbox)
        {
            this.buttonBoundingbox = buttonBoundingbox;
        }

        /// <summary>
        /// button update
        /// </summary>
        /// <param name="mouseState">the current MouseState</param>
        public void Update(Microsoft.Xna.Framework.Input.MouseState mouseState){

            // if there are click listener check if the mouse is over the button
            if (this.onClick != null && this.onClick.GetInvocationList().Length > 0)
            {
                this.mouseBoundingbox.X = mouseState.X;
                this.mouseBoundingbox.Y = mouseState.Y;

                if (this.buttonBoundingbox.Intersects(this.mouseBoundingbox))
                {
                    this.color = defaultHover;

                    if (FenrirGame.Instance.Properties.Input.LeftClick)
                        this.OnClick(new EventArgs());
                }
                else
                {
                    if (this.isActive)
                        this.color = defaultActive;
                    else
                        this.color = defaultColor;
                }
            }
        }

        /// <summary>
        /// on click handle thing
        /// </summary>
        /// <param name="e">the click event</param>
        protected virtual void OnClick(EventArgs e)
        {
            if (this.toggle && !this.isActive)
                this.isActive = true;
            else if (this.toggle && this.isActive)
                this.isActive = false;

            EventHandler handler = onClick;
            if (handler != null)
                handler(this, e);
        }

        /// <summary>
        /// basic draw method .. does nothing - implement it! ;)
        /// </summary>
        public virtual void Draw() { }
    }
}