namespace PolygonGubarkov
{
    partial class GeneralForm
    {
        /// <summary>
        /// Требуется переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Обязательный метод для поддержки конструктора - не изменяйте
        /// содержимое данного метода при помощи редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.файлToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.открытьToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.сохранитьToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.операцииToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.замкнутьЛоманнуюToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.залитьВыбраннымЦветомToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripComboBoxBrushes = new System.Windows.Forms.ToolStripComboBox();
            this.показатьМассивыДанныхToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.перестроитьКартуToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.clearToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.labToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.selfIntersection = new System.Windows.Forms.ToolStripMenuItem();
            this.affiliation = new System.Windows.Forms.ToolStripMenuItem();
            this.polygonsItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pointDistance = new System.Windows.Forms.ToolStripMenuItem();
            this.polygonEdit = new System.Windows.Forms.ToolStripMenuItem();
            this.regimeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.polygonCreating = new System.Windows.Forms.ToolStripMenuItem();
            this.navigation = new System.Windows.Forms.ToolStripMenuItem();
            this.polygonEditItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.rebuildPolygons = new System.Windows.Forms.ToolStripMenuItem();
            this.editedPolygon = new System.Windows.Forms.ToolStripComboBox();
            this.objectsTypes = new System.Windows.Forms.ToolStripComboBox();
            this.colorComboBox = new System.Windows.Forms.ToolStripComboBox();
            this.создатьToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.редактироватьToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.удалитьToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.help = new System.Windows.Forms.ToolStripStatusLabel();
            this.panelGraph = new System.Windows.Forms.Panel();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.textBoxEdit = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.richTextBoxLog = new System.Windows.Forms.RichTextBox();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.menuStrip1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.panelGraph.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.файлToolStripMenuItem,
            this.операцииToolStripMenuItem,
            this.clearToolStripMenuItem,
            this.labToolStripMenuItem,
            this.regimeToolStripMenuItem,
            this.editedPolygon,
            this.objectsTypes,
            this.colorComboBox,
            this.создатьToolStripMenuItem,
            this.редактироватьToolStripMenuItem,
            this.удалитьToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(969, 27);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // файлToolStripMenuItem
            // 
            this.файлToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.открытьToolStripMenuItem,
            this.сохранитьToolStripMenuItem});
            this.файлToolStripMenuItem.Name = "файлToolStripMenuItem";
            this.файлToolStripMenuItem.Size = new System.Drawing.Size(48, 23);
            this.файлToolStripMenuItem.Text = "Файл";
            // 
            // открытьToolStripMenuItem
            // 
            this.открытьToolStripMenuItem.Name = "открытьToolStripMenuItem";
            this.открытьToolStripMenuItem.Size = new System.Drawing.Size(132, 22);
            this.открытьToolStripMenuItem.Text = "Открыть";
            this.открытьToolStripMenuItem.Click += new System.EventHandler(this.открытьToolStripMenuItem_Click);
            // 
            // сохранитьToolStripMenuItem
            // 
            this.сохранитьToolStripMenuItem.Name = "сохранитьToolStripMenuItem";
            this.сохранитьToolStripMenuItem.Size = new System.Drawing.Size(132, 22);
            this.сохранитьToolStripMenuItem.Text = "Сохранить";
            this.сохранитьToolStripMenuItem.Click += new System.EventHandler(this.сохранитьToolStripMenuItem_Click);
            // 
            // операцииToolStripMenuItem
            // 
            this.операцииToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.замкнутьЛоманнуюToolStripMenuItem,
            this.залитьВыбраннымЦветомToolStripMenuItem,
            this.показатьМассивыДанныхToolStripMenuItem,
            this.перестроитьКартуToolStripMenuItem});
            this.операцииToolStripMenuItem.Name = "операцииToolStripMenuItem";
            this.операцииToolStripMenuItem.Size = new System.Drawing.Size(75, 23);
            this.операцииToolStripMenuItem.Text = "Операции";
            // 
            // замкнутьЛоманнуюToolStripMenuItem
            // 
            this.замкнутьЛоманнуюToolStripMenuItem.Name = "замкнутьЛоманнуюToolStripMenuItem";
            this.замкнутьЛоманнуюToolStripMenuItem.Size = new System.Drawing.Size(225, 22);
            this.замкнутьЛоманнуюToolStripMenuItem.Text = "Замкнуть ломанную";
            this.замкнутьЛоманнуюToolStripMenuItem.Click += new System.EventHandler(this.замкнутьЛоманнуюToolStripMenuItem_Click);
            // 
            // залитьВыбраннымЦветомToolStripMenuItem
            // 
            this.залитьВыбраннымЦветомToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripComboBoxBrushes});
            this.залитьВыбраннымЦветомToolStripMenuItem.Name = "залитьВыбраннымЦветомToolStripMenuItem";
            this.залитьВыбраннымЦветомToolStripMenuItem.Size = new System.Drawing.Size(225, 22);
            this.залитьВыбраннымЦветомToolStripMenuItem.Text = "Залить выбранным цветом";
            this.залитьВыбраннымЦветомToolStripMenuItem.Click += new System.EventHandler(this.залитьВыбраннымЦветомToolStripMenuItem_Click);
            // 
            // toolStripComboBoxBrushes
            // 
            this.toolStripComboBoxBrushes.Name = "toolStripComboBoxBrushes";
            this.toolStripComboBoxBrushes.Size = new System.Drawing.Size(121, 23);
            // 
            // показатьМассивыДанныхToolStripMenuItem
            // 
            this.показатьМассивыДанныхToolStripMenuItem.Name = "показатьМассивыДанныхToolStripMenuItem";
            this.показатьМассивыДанныхToolStripMenuItem.Size = new System.Drawing.Size(225, 22);
            this.показатьМассивыДанныхToolStripMenuItem.Text = "Показать массивы данных";
            this.показатьМассивыДанныхToolStripMenuItem.Click += new System.EventHandler(this.показатьМассивыДанныхToolStripMenuItem_Click);
            // 
            // перестроитьКартуToolStripMenuItem
            // 
            this.перестроитьКартуToolStripMenuItem.Name = "перестроитьКартуToolStripMenuItem";
            this.перестроитьКартуToolStripMenuItem.Size = new System.Drawing.Size(225, 22);
            this.перестроитьКартуToolStripMenuItem.Text = "Перестроить карту";
            this.перестроитьКартуToolStripMenuItem.Click += new System.EventHandler(this.перестроитьКартуToolStripMenuItem_Click);
            // 
            // clearToolStripMenuItem
            // 
            this.clearToolStripMenuItem.Name = "clearToolStripMenuItem";
            this.clearToolStripMenuItem.Size = new System.Drawing.Size(71, 23);
            this.clearToolStripMenuItem.Text = "Очистить";
            this.clearToolStripMenuItem.Click += new System.EventHandler(this.clearToolStripMenuItem_Click_1);
            // 
            // labToolStripMenuItem
            // 
            this.labToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.selfIntersection,
            this.affiliation,
            this.polygonsItem,
            this.pointDistance,
            this.polygonEdit});
            this.labToolStripMenuItem.Name = "labToolStripMenuItem";
            this.labToolStripMenuItem.Size = new System.Drawing.Size(38, 23);
            this.labToolStripMenuItem.Text = "Lab";
            this.labToolStripMenuItem.Visible = false;
            // 
            // selfIntersection
            // 
            this.selfIntersection.Name = "selfIntersection";
            this.selfIntersection.Size = new System.Drawing.Size(159, 22);
            this.selfIntersection.Text = "self-intersection";
            this.selfIntersection.ToolTipText = "Проверка многогранника на самопересекаемость";
            this.selfIntersection.Click += new System.EventHandler(this.selfintersectionToolStripMenuItem_Click);
            // 
            // affiliation
            // 
            this.affiliation.Name = "affiliation";
            this.affiliation.Size = new System.Drawing.Size(159, 22);
            this.affiliation.Text = "affiliation";
            this.affiliation.ToolTipText = "Проверка принадлежности точки многораннику";
            this.affiliation.Click += new System.EventHandler(this.selfintersectionToolStripMenuItem_Click);
            // 
            // polygonsItem
            // 
            this.polygonsItem.Name = "polygonsItem";
            this.polygonsItem.Size = new System.Drawing.Size(159, 22);
            this.polygonsItem.Text = "polygons";
            this.polygonsItem.ToolTipText = "Много многогранников, самопересекающиеся подсвечивать другим цветом";
            this.polygonsItem.Click += new System.EventHandler(this.selfintersectionToolStripMenuItem_Click);
            // 
            // pointDistance
            // 
            this.pointDistance.Name = "pointDistance";
            this.pointDistance.Size = new System.Drawing.Size(159, 22);
            this.pointDistance.Text = "point distance";
            this.pointDistance.ToolTipText = "Кратчайшее растояние от точки до многоугольника";
            this.pointDistance.Click += new System.EventHandler(this.selfintersectionToolStripMenuItem_Click);
            // 
            // polygonEdit
            // 
            this.polygonEdit.Checked = true;
            this.polygonEdit.CheckState = System.Windows.Forms.CheckState.Checked;
            this.polygonEdit.Name = "polygonEdit";
            this.polygonEdit.Size = new System.Drawing.Size(159, 22);
            this.polygonEdit.Text = "polygon edit";
            this.polygonEdit.ToolTipText = "Редактирует многогранники. Работает с уже созданными объектами.";
            this.polygonEdit.Click += new System.EventHandler(this.selfintersectionToolStripMenuItem_Click);
            // 
            // regimeToolStripMenuItem
            // 
            this.regimeToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.polygonCreating,
            this.navigation,
            this.polygonEditItem,
            this.toolStripMenuItem1,
            this.rebuildPolygons});
            this.regimeToolStripMenuItem.Name = "regimeToolStripMenuItem";
            this.regimeToolStripMenuItem.Size = new System.Drawing.Size(59, 23);
            this.regimeToolStripMenuItem.Text = "Regime";
            this.regimeToolStripMenuItem.Visible = false;
            // 
            // polygonCreating
            // 
            this.polygonCreating.Checked = true;
            this.polygonCreating.CheckState = System.Windows.Forms.CheckState.Checked;
            this.polygonCreating.Name = "polygonCreating";
            this.polygonCreating.Size = new System.Drawing.Size(166, 22);
            this.polygonCreating.Text = "Polygon creating";
            this.polygonCreating.Click += new System.EventHandler(this.polygonCreating_Click);
            // 
            // navigation
            // 
            this.navigation.Name = "navigation";
            this.navigation.Size = new System.Drawing.Size(166, 22);
            this.navigation.Text = "Navigation";
            this.navigation.Click += new System.EventHandler(this.polygonCreating_Click);
            // 
            // polygonEditItem
            // 
            this.polygonEditItem.Name = "polygonEditItem";
            this.polygonEditItem.Size = new System.Drawing.Size(166, 22);
            this.polygonEditItem.Text = "Polygon Edit";
            this.polygonEditItem.Click += new System.EventHandler(this.polygonCreating_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(163, 6);
            // 
            // rebuildPolygons
            // 
            this.rebuildPolygons.Name = "rebuildPolygons";
            this.rebuildPolygons.Size = new System.Drawing.Size(166, 22);
            this.rebuildPolygons.Text = "Rebuild polygons";
            this.rebuildPolygons.Click += new System.EventHandler(this.rebuildPolygonsToolStripMenuItem_Click);
            // 
            // editedPolygon
            // 
            this.editedPolygon.Name = "editedPolygon";
            this.editedPolygon.Size = new System.Drawing.Size(121, 23);
            this.editedPolygon.ToolTipText = "Выбор редактируемого многоугольника";
            this.editedPolygon.SelectedIndexChanged += new System.EventHandler(this.editedPolygon_SelectedIndexChanged);
            // 
            // objectsTypes
            // 
            this.objectsTypes.Name = "objectsTypes";
            this.objectsTypes.Size = new System.Drawing.Size(121, 23);
            this.objectsTypes.SelectedIndexChanged += new System.EventHandler(this.objectsTypes_SelectedIndexChanged);
            // 
            // colorComboBox
            // 
            this.colorComboBox.Name = "colorComboBox";
            this.colorComboBox.Size = new System.Drawing.Size(121, 23);
            this.colorComboBox.TextChanged += new System.EventHandler(this.colorComboBox_TextChanged);
            // 
            // создатьToolStripMenuItem
            // 
            this.создатьToolStripMenuItem.Name = "создатьToolStripMenuItem";
            this.создатьToolStripMenuItem.Size = new System.Drawing.Size(62, 23);
            this.создатьToolStripMenuItem.Text = "Создать";
            this.создатьToolStripMenuItem.Click += new System.EventHandler(this.создатьToolStripMenuItem_Click);
            // 
            // редактироватьToolStripMenuItem
            // 
            this.редактироватьToolStripMenuItem.Name = "редактироватьToolStripMenuItem";
            this.редактироватьToolStripMenuItem.Size = new System.Drawing.Size(99, 23);
            this.редактироватьToolStripMenuItem.Text = "Редактировать";
            this.редактироватьToolStripMenuItem.Click += new System.EventHandler(this.редактироватьToolStripMenuItem_Click);
            // 
            // удалитьToolStripMenuItem
            // 
            this.удалитьToolStripMenuItem.Name = "удалитьToolStripMenuItem";
            this.удалитьToolStripMenuItem.Size = new System.Drawing.Size(63, 23);
            this.удалитьToolStripMenuItem.Text = "Удалить";
            this.удалитьToolStripMenuItem.Click += new System.EventHandler(this.удалитьToolStripMenuItem_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.help});
            this.statusStrip1.Location = new System.Drawing.Point(0, 446);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(969, 22);
            this.statusStrip1.TabIndex = 1;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // help
            // 
            this.help.Name = "help";
            this.help.Size = new System.Drawing.Size(118, 17);
            this.help.Text = "toolStripStatusLabel1";
            // 
            // panelGraph
            // 
            this.panelGraph.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelGraph.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelGraph.Controls.Add(this.pictureBox1);
            this.panelGraph.Controls.Add(this.textBoxEdit);
            this.panelGraph.Controls.Add(this.label1);
            this.panelGraph.Location = new System.Drawing.Point(10, 30);
            this.panelGraph.Name = "panelGraph";
            this.panelGraph.Size = new System.Drawing.Size(741, 413);
            this.panelGraph.TabIndex = 2;
            this.panelGraph.MouseClick += new System.Windows.Forms.MouseEventHandler(this.panelGraph_MouseClick);
            this.panelGraph.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.panelGraph_MouseDoubleClick);
            this.panelGraph.MouseDown += new System.Windows.Forms.MouseEventHandler(this.panelGraph_MouseDown);
            this.panelGraph.MouseHover += new System.EventHandler(this.panelGraph_MouseHover);
            this.panelGraph.MouseUp += new System.Windows.Forms.MouseEventHandler(this.panelGraph_MouseUp);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Location = new System.Drawing.Point(335, 3);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(401, 405);
            this.pictureBox1.TabIndex = 2;
            this.pictureBox1.TabStop = false;
            // 
            // textBoxEdit
            // 
            this.textBoxEdit.Location = new System.Drawing.Point(6, 25);
            this.textBoxEdit.Name = "textBoxEdit";
            this.textBoxEdit.Size = new System.Drawing.Size(100, 20);
            this.textBoxEdit.TabIndex = 1;
            this.textBoxEdit.Text = "Введите текст...";
            this.textBoxEdit.Visible = false;
            this.textBoxEdit.VisibleChanged += new System.EventHandler(this.textBoxEdit_VisibleChanged);
            this.textBoxEdit.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBoxEdit_KeyDown);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.Color.Black;
            this.label1.Location = new System.Drawing.Point(3, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "label1";
            this.label1.Visible = false;
            this.label1.DragDrop += new System.Windows.Forms.DragEventHandler(this.label1_DragDrop);
            this.label1.MouseClick += new System.Windows.Forms.MouseEventHandler(this.label1_MouseClick);
            this.label1.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.label1_MouseDoubleClick);
            this.label1.MouseMove += new System.Windows.Forms.MouseEventHandler(this.label1_MouseMove);
            // 
            // richTextBoxLog
            // 
            this.richTextBoxLog.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.richTextBoxLog.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.richTextBoxLog.Location = new System.Drawing.Point(757, 30);
            this.richTextBoxLog.Name = "richTextBoxLog";
            this.richTextBoxLog.ReadOnly = true;
            this.richTextBoxLog.Size = new System.Drawing.Size(212, 413);
            this.richTextBoxLog.TabIndex = 3;
            this.richTextBoxLog.Text = "";
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // GeneralForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(969, 468);
            this.Controls.Add(this.richTextBoxLog);
            this.Controls.Add(this.panelGraph);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "GeneralForm";
            this.Text = "Polygon";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.panelGraph.ResumeLayout(false);
            this.panelGraph.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem clearToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem labToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem selfIntersection;
        private System.Windows.Forms.ToolStripMenuItem affiliation;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel help;
        private System.Windows.Forms.Panel panelGraph;
        private System.Windows.Forms.ToolStripMenuItem regimeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem polygonCreating;
        private System.Windows.Forms.ToolStripMenuItem navigation;
        private System.Windows.Forms.ToolStripMenuItem polygonsItem;
        private System.Windows.Forms.ToolStripMenuItem pointDistance;
        private System.Windows.Forms.ToolStripMenuItem polygonEdit;
        private System.Windows.Forms.ToolStripMenuItem rebuildPolygons;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
        private System.Windows.Forms.ToolStripComboBox editedPolygon;
        private System.Windows.Forms.ToolStripMenuItem polygonEditItem;
        private System.Windows.Forms.ToolStripComboBox objectsTypes;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ToolStripComboBox colorComboBox;
        private System.Windows.Forms.TextBox textBoxEdit;
        private System.Windows.Forms.ToolStripMenuItem операцииToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem создатьToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem редактироватьToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem удалитьToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem замкнутьЛоманнуюToolStripMenuItem;
        private System.Windows.Forms.RichTextBox richTextBoxLog;
        private System.Windows.Forms.ToolStripMenuItem показатьМассивыДанныхToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem файлToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem открытьToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem сохранитьToolStripMenuItem;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.ToolStripMenuItem залитьВыбраннымЦветомToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem перестроитьКартуToolStripMenuItem;
        private System.Windows.Forms.ToolStripComboBox toolStripComboBoxBrushes;
        private System.Windows.Forms.PictureBox pictureBox1;


    }
}

