using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing.Drawing2D;

namespace cons{
[DefaultEvent("_TextChanged")]public  class InputBox : UserControl{
 private Color borderColor = Color.MediumSlateBlue;Color blockcolor=Color.White;
private int borderSize = 2;
private bool underlinedStyle = false;
private Color borderFocusColor = Color.HotPink;
private bool isFocused = false;
private bool _isLabeled=true;
private int _brRds=0;
private int _rds=0;
private Padding _inPad=new Padding(10,10,10,10);
private Padding _otPad=new Padding(10,10,10,10);

public InputBox(){InitializeComponent(); }

public event EventHandler _TextChanged;

[Category("Ext")]public Color BorderColor{get{return borderColor; }set{borderColor=value;this.Invalidate(); } }

[Category("Ext")]public Color BlockColor{get{return borderColor; }set{borderColor=value;this.Invalidate(); } }

[Category("Ext")]public int BorderSize{get{return borderSize; }set{borderSize=value;this.Invalidate(); } }

[Category("Ext")]public bool UnderlinedStyle{get{return underlinedStyle; }set{underlinedStyle=value;this.Invalidate(); } }

[Category("Ext")]public bool PasswordChar{get {return txtb.UseSystemPasswordChar; }
set { txtb.UseSystemPasswordChar = value; } }
[Category("Ext")]public bool Multiline{get{return txtb.Multiline; }set{txtb.Multiline=value; } }

[Category("Ext")]public override Color BackColor{get{return base.BackColor; }
set{base.BackColor = value;txtb.BackColor = value; } }

[Category("Ext")]public override Color ForeColor{get{return base.ForeColor; }
set{base.ForeColor = value;txtb.ForeColor = value; } }

[Category("Ext")]public override Font Font{get{return base.Font; }
set{base.Font = value;lblb.Font=txtb.Font=value;if (this.DesignMode)updateRender(); } }

[Category("Ext")]public new Padding Padding{get{return base.Padding; }
set{base.Padding=IntPadding+OutPadding;DesRld(); } }

[Category("Ext")]
public new Padding IntPadding{get{return _inPad; }set{
value.Left=value.Left>=borderSize?value.Left:borderSize;
value.Top=value.Top>=borderSize?value.Top:borderSize;
value.Right=value.Right>=borderSize?value.Right:borderSize;
value.Bottom=value.Bottom>=borderSize?value.Bottom:borderSize;
_inPad=value;base.Padding=_inPad+_otPad;DesRld(); } }

[Category("Ext")]
public new Padding OutPadding{get{return _otPad; }set{_otPad=value;base.Padding=_inPad+_otPad;DesRld(); } }

[Category("Ext")]public string Texts{get{return txtb.Text; }set{txtb.Text=value; } }

[Category("Ext")]public Color BorderFocusColor{get{return borderFocusColor; }set{borderFocusColor=value; } }

[Category("Ext")]
public string Placeholder{get{return txtb.Placeholder; }set{txtb.Placeholder=value; } }

[Category("Ext")]
public Color PlaceholderColor{get{return txtb.PlaceholderColor; }set{txtb.PlaceholderColor=value; } }

[Category("Ext")]public bool isLabeled{get {return _isLabeled; }set{_isLabeled=value;updateRender();this.lblb.Visible=_isLabeled;this.Invalidate(); } }
[Category("Ext")]public string Label{get {return lblb.Text; }set{lblb.Text=value;updateRender(); } }
[Category("Ext")]public Color LabelColor{get{return lblb.ForeColor; }set{lblb.ForeColor=value;updateRender(); } }
[Category("Ext")]public int MaxLength{get{return txtb.MaxLength; }set{txtb.MaxLength=value; } }
[Category("Ext")]public int BorderRadius{get{return _brRds; }set{int mr=rgdHeight()/2;_brRds=mr>=value?value:mr;DesRld(); } }
[Category("Ext")]public int Radius{get{return _rds; }set{int mr=this.Size.Height/2;_rds=mr>=value?value:mr;DesRld(); } }
[Category("Ext")]public bool ReadOnly{get{return txtb.ReadOnly; }set{txtb.ReadOnly=value; } }
  //Overridden methods

protected override void OnPaint(PaintEventArgs e){base.OnPaint(e);Graphics g = e.Graphics;
int txtHeight=GetTxtHeight();int ttlTh=GetTxtTHeight();
int x1=this._otPad.Left;int x2=this.Width-this._otPad.Right;
int y1=isLabeled?txtHeight/2+_otPad.Top:_otPad.Top;int y2=Height-_otPad.Bottom;
Size rhw=GetRhw(x1,y1,x2,y2);
int ye=this.Height-y1-this.Padding.Bottom/3;
if((_brRds>0||_rds>0)&&!underlinedStyle){
var rrb =new Rectangle();rrb.X=x1;rrb.Y=y1;rrb.Width=rhw.Width;rrb.Height=rhw.Height;
var rob =new Rectangle(0,0,this.Width,this.Height);
 var rectBorder=Rectangle.Inflate(rrb, -borderSize, -borderSize);
int smoothSize = borderSize > 0 ? borderSize : 1;
using (GraphicsPath pathBorderSmooth=RoundedRect(rob,_rds))
using (GraphicsPath pathBorder=RoundedRect(rrb,_brRds))
using (Pen penBorderSmooth=new Pen(this.BackColor, smoothSize))
using (Pen penBorder = new Pen(borderColor, borderSize)){
this.Region = new Region(pathBorderSmooth);
g.SmoothingMode = SmoothingMode.AntiAlias;
penBorder.Alignment = System.Drawing.Drawing2D.PenAlignment.Center;
if (isFocused) penBorder.Color = borderFocusColor;
g.DrawPath(penBorderSmooth, pathBorderSmooth);
g.DrawPath(penBorder, pathBorder);  } }

else{using(Pen penBorder=new Pen(borderColor, borderSize)){
penBorder.Alignment=System.Drawing.Drawing2D.PenAlignment.Inset;
if (isFocused)penBorder.Color=borderFocusColor;
int yee=y2;//ttlTh+y1-this.Padding.Bottom/3;
if (underlinedStyle)g.DrawLine(penBorder,x1,y2,x2,y2);
else g.DrawRectangle(penBorder,x1,y1,rhw.Width,rhw.Height); } }

 }

private Size GetRhw(int x1,int y1,int x2,int y2){Size a=new Size();a.Width=x2-x1;a.Height=y2-y1;return a; }
/*private GraphicsPath GetFigurePath(Rectangle rect, int radius){
GraphicsPath path = new GraphicsPath();float curveSize = radius * 2F;path.StartFigure();
path.AddArc(rect.X, rect.Y, curveSize, curveSize, 180, 90);
path.AddArc(rect.Right - curveSize, rect.Y, curveSize, curveSize, 270, 90);
path.AddArc(rect.Right - curveSize, rect.Bottom - curveSize, curveSize, curveSize, 0, 90);
path.AddArc(rect.X, rect.Bottom - curveSize, curveSize, curveSize, 90, 90);
path.CloseFigure();return path; }*/
protected override void OnResize(EventArgs e){base.OnResize(e);if(this.DesignMode)updateRender(); }
protected override void OnPaddingChanged(EventArgs e){base.OnResize(e);if(this.DesignMode)updateRender(); }


public static GraphicsPath RoundedRect(Rectangle bounds,int radius){
int d=radius*2;Size size = new Size(d,d);
Rectangle arc = new Rectangle(bounds.Location, size);
GraphicsPath path = new GraphicsPath();
if (radius == 0){path.AddRectangle(bounds);return path; }
path.AddArc(arc, 180, 90);arc.X =bounds.Right-d;
path.AddArc(arc, 270, 90);arc.Y=bounds.Bottom-d;
path.AddArc(arc, 0, 90);arc.X = bounds.Left;
path.AddArc(arc, 90, 90);path.CloseFigure();return path; }

protected override void OnLoad(EventArgs e){base.OnLoad(e);updateRender(); }

        //Private methods
private void updateRender(){
int txtHeight=GetTxtHeight();int ttlTh=GetTxtTHeight();
int x1=this.Padding.Left;int x2=this.Width-x1-this.Padding.Right;
int y1=isLabeled?txtHeight+this.Padding.Top:this.Padding.Top;
int y2=isLabeled?rgdHeight()-txtHeight:rgdHeight();
if(!txtb.Multiline){
txtb.Multiline=true;txtb.MinimumSize = new Size(0, txtHeight);txtb.Multiline = false;
this.Height=ttlTh+(isLabeled?txtHeight:0);
this.txtb.Size = new System.Drawing.Size(x2,txtHeight); }else{
this.txtb.Size = new System.Drawing.Size(x2,y2); }

if(isLabeled){
this.lblb.Location=new System.Drawing.Point(this.Padding.Left*2,_otPad.Top);
this.lblb.Size=new System.Drawing.Size(GetTxtWidth(lblb.Text)+5,txtHeight); };
this.txtb.Location=new System.Drawing.Point(x1,y1); 


}

private int GetTxtWidth(string a){return TextRenderer.MeasureText(a,this.Font).Width; }
private int GetTxtHeight(string a){return TextRenderer.MeasureText(a,this.Font).Height; }
private int GetTxtHeight(){return TextRenderer.MeasureText("T",this.Font).Height; }
private int GetTxtTHeight(){return TextRenderer.MeasureText("T",this.Font).Height+Padding.Top+Padding.Bottom; }
private int rgdWidth(){return this.Width-this.Padding.Left-this.Padding.Right; }
private int rgdHeight(){return this.Height-this.Padding.Top-this.Padding.Bottom; }

private void DesRld(){if(this.DesignMode){updateRender();this.Invalidate();if(this.DesignMode)updateRender(); } }

private System.ComponentModel.IContainer components = null;

protected override void Dispose(bool disposing){if(disposing&&(components!=null)){
components.Dispose(); } base.Dispose(disposing); }

  #region Component Designer generated code

  /// <summary> 
  /// Required method for Designer support - do not modify 
  /// the contents of this method with the code editor.
  /// </summary>
private void InitializeComponent(){
   this.txtb = new cons.TxtBox();
   this.lblb = new System.Windows.Forms.Label();
   this.SuspendLayout();
   // 
   // txtb
   // 
   this.txtb.BorderStyle = System.Windows.Forms.BorderStyle.None;
   this.txtb.Location =new System.Drawing.Point(this.Padding.Left,isLabeled?(int)(GetTxtHeight()/1.33)/4:this.Padding.Top);
   this.txtb.Name = "txtb";
this.txtb.BackColor=this.BackColor;
this.txtb.ForeColor=this.ForeColor;
   this.txtb.Placeholder = "Ent Txt";
   this.txtb.PlaceholderColor = System.Drawing.Color.Gray;
   this.txtb.Size = new System.Drawing.Size(rgdWidth(), 15);
   this.txtb.TabIndex = 0;
this.txtb.Click +=(s,e)=>{this.OnClick(e); };
this.txtb.TextChanged +=(s,e)=>{if(_TextChanged!=null)_TextChanged.Invoke(s,e); };
this.txtb.Enter +=(s,e)=>{isFocused = true;this.Invalidate(); };
this.txtb.KeyPress +=(s,e)=> this.OnKeyPress(e);
this.txtb.Leave +=(s,e)=>{isFocused = false;this.Invalidate();};
this.txtb.MouseEnter +=(s,e)=>{this.OnMouseEnter(e); };
this.txtb.MouseLeave +=(s,e)=>this.OnMouseLeave(e);
   // 
   // lblb
   // 
//this.lblb.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) | System.Windows.Forms.AnchorStyles.Left)));
   this.lblb.Text = "Titl";
   this.lblb.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
   this.lblb.ForeColor = System.Drawing.Color.DarkCyan;
   this.lblb.Location = new System.Drawing.Point(this.Padding.Left*2,3);
   this.lblb.Name = "lblb";
   this.lblb.Size = new System.Drawing.Size(GetTxtWidth("Title"), GetTxtHeight());
   this.lblb.TabIndex = 0;
this.lblb.Visible=isLabeled;
   // 
   // InputBox
   // 
   this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
   this.BackColor = System.Drawing.SystemColors.Window;
   this.Controls.Add(this.lblb);this.Controls.Add(this.txtb);
   this.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
   this.ForeColor = System.Drawing.Color.DimGray;
   this.Margin = new System.Windows.Forms.Padding(4);
   this.Name = "InputBox";
   this.Padding = new System.Windows.Forms.Padding(7);
   this.Size = new System.Drawing.Size(250, 57);
   this.ResumeLayout(false);
   this.PerformLayout();

}

  #endregion

  private TxtBox txtb;
  private Label lblb;

 }

class TxtBox:TextBox{private string _placeholder="Ent Txt";private Color _plccol=Color.Gray;
 [Category("Ext")]
public string Placeholder{get{ return _placeholder; }set{_placeholder=value;this.Invalidate(); } }
 [Category("Ext")]
public Color PlaceholderColor{get{return _plccol; }set{_plccol=value;this.Invalidate(); } }
protected override void OnPaint(PaintEventArgs e){base.OnPaint(e);
if(string.IsNullOrEmpty(this.Text.Trim())&&!string.IsNullOrEmpty(this._placeholder) &&!this.Focused){
Point startingPoint = new Point(0, 0);StringFormat f=new StringFormat();
Font font = new Font(this.Font.FontFamily.Name,this.Font.Size,FontStyle.Italic|FontStyle.Bold);
if(this.RightToLeft==RightToLeft.Yes){f.LineAlignment = StringAlignment.Far;
f.FormatFlags=StringFormatFlags.DirectionRightToLeft; }
SolidBrush b = new SolidBrush(_plccol);
e.Graphics.DrawString(_placeholder, font,b,this.ClientRectangle,f); } }

const int WM_PAINT = 0x000F;
protected override void WndProc(ref Message m){base.WndProc(ref m);if(m.Msg == WM_PAINT){
this.OnPaint(new PaintEventArgs(Graphics.FromHwnd(m.HWnd),this.ClientRectangle)); } }
  
 }
}
