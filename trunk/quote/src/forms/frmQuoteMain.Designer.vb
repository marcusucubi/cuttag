<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmQuoteMain
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
		Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmQuoteMain))
		Me.mnuQuote = New System.Windows.Forms.MenuStrip
		Me.mniOpen = New System.Windows.Forms.ToolStripMenuItem
		Me.mniImport = New System.Windows.Forms.ToolStripMenuItem
		Me.tabQuote = New System.Windows.Forms.TabControl
		Me.tpgFind = New System.Windows.Forms.TabPage
		Me.pnlSearchControls = New System.Windows.Forms.Panel
		Me.lblSelectAndShow = New System.Windows.Forms.Label
		Me.lblSearchInstructions = New System.Windows.Forms.Label
		Me.lblPartNumberLookup = New System.Windows.Forms.Label
		Me.lblRFQLookup = New System.Windows.Forms.Label
		Me.lblQuoteNumberLookup = New System.Windows.Forms.Label
		Me.btnSeach = New System.Windows.Forms.Button
		Me.txtRFQLookup = New System.Windows.Forms.TextBox
		Me.txtQuoteNumberLookup = New System.Windows.Forms.TextBox
		Me.txtPartNumberLookup = New System.Windows.Forms.TextBox
		Me.dgvSearchResults = New System.Windows.Forms.DataGridView
		Me.tpgShow = New System.Windows.Forms.TabPage
		Me.lblUnitPrice = New System.Windows.Forms.Label
		Me.txtUnitPrice = New System.Windows.Forms.TextBox
		Me.lblComputedPrice = New System.Windows.Forms.Label
		Me.lblTimeMultiplier = New System.Windows.Forms.Label
		Me.lblCuWeightMultiplier = New System.Windows.Forms.Label
		Me.lblCuWeight = New System.Windows.Forms.Label
		Me.lblCuPrice = New System.Windows.Forms.Label
		Me.lblBoxPrice = New System.Windows.Forms.Label
		Me.lblBoxSize = New System.Windows.Forms.Label
		Me.lblVerifiedBy = New System.Windows.Forms.Label
		Me.lblCreatedBy = New System.Windows.Forms.Label
		Me.lblWireTime = New System.Windows.Forms.Label
		Me.lblCuts = New System.Windows.Forms.Label
		Me.lblTooling = New System.Windows.Forms.Label
		Me.lblFormBoardCost = New System.Windows.Forms.Label
		Me.lblLaborMinutes = New System.Windows.Forms.Label
		Me.lblCutTime = New System.Windows.Forms.Label
		Me.lblMinimum = New System.Windows.Forms.Label
		Me.lblEAU = New System.Windows.Forms.Label
		Me.lblContactName = New System.Windows.Forms.Label
		Me.lblPartNumber = New System.Windows.Forms.Label
		Me.lblLaborRate = New System.Windows.Forms.Label
		Me.lblQuoteDate = New System.Windows.Forms.Label
		Me.lblRFQ = New System.Windows.Forms.Label
		Me.lblShipping = New System.Windows.Forms.Label
		Me.lblQuoteNumber = New System.Windows.Forms.Label
		Me.txtComputedPrice = New System.Windows.Forms.TextBox
		Me.txtPartRevision = New System.Windows.Forms.TextBox
		Me.txtBoxSize = New System.Windows.Forms.TextBox
		Me.txtBoxPrice = New System.Windows.Forms.TextBox
		Me.txtCuPrice = New System.Windows.Forms.TextBox
		Me.txtCuWeight = New System.Windows.Forms.TextBox
		Me.txtCuWeightMultiplier = New System.Windows.Forms.TextBox
		Me.txtTimeMultiplier = New System.Windows.Forms.TextBox
		Me.txtVerifiedBy = New System.Windows.Forms.TextBox
		Me.txtFormBoardCost = New System.Windows.Forms.TextBox
		Me.txtWireTime = New System.Windows.Forms.TextBox
		Me.txtCutTime = New System.Windows.Forms.TextBox
		Me.txtCuts = New System.Windows.Forms.TextBox
		Me.txtCreatedBy = New System.Windows.Forms.TextBox
		Me.txtContactName = New System.Windows.Forms.TextBox
		Me.txtRFQ = New System.Windows.Forms.TextBox
		Me.txtQuoteDate = New System.Windows.Forms.TextBox
		Me.txtPartNumber = New System.Windows.Forms.TextBox
		Me.txtMinimum = New System.Windows.Forms.TextBox
		Me.txtLaborMinutes = New System.Windows.Forms.TextBox
		Me.txtLaborRate = New System.Windows.Forms.TextBox
		Me.txtTooling = New System.Windows.Forms.TextBox
		Me.txtShipping = New System.Windows.Forms.TextBox
		Me.txtEAU = New System.Windows.Forms.TextBox
		Me.txtQuoteNumber = New System.Windows.Forms.TextBox
		Me.mnuQuote.SuspendLayout()
		Me.tabQuote.SuspendLayout()
		Me.tpgFind.SuspendLayout()
		Me.pnlSearchControls.SuspendLayout()
		CType(Me.dgvSearchResults, System.ComponentModel.ISupportInitialize).BeginInit()
		Me.tpgShow.SuspendLayout()
		Me.SuspendLayout()
		'
		'mnuQuote
		'
		Me.mnuQuote.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mniOpen, Me.mniImport})
		Me.mnuQuote.Location = New System.Drawing.Point(0, 0)
		Me.mnuQuote.Name = "mnuQuote"
		Me.mnuQuote.Size = New System.Drawing.Size(610, 24)
		Me.mnuQuote.TabIndex = 0
		Me.mnuQuote.Text = "mnuQuote"
		'
		'mniOpen
		'
		Me.mniOpen.Name = "mniOpen"
		Me.mniOpen.Size = New System.Drawing.Size(45, 20)
		Me.mniOpen.Text = "&Open"
		Me.mniOpen.ToolTipText = "Open existing quote"
		'
		'mniImport
		'
		Me.mniImport.Name = "mniImport"
		Me.mniImport.Size = New System.Drawing.Size(48, 20)
		Me.mniImport.Text = "&Import"
		Me.mniImport.ToolTipText = "Import Excel Quote"
		'
		'tabQuote
		'
		Me.tabQuote.Controls.Add(Me.tpgFind)
		Me.tabQuote.Controls.Add(Me.tpgShow)
		Me.tabQuote.Location = New System.Drawing.Point(4, 25)
		Me.tabQuote.Name = "tabQuote"
		Me.tabQuote.SelectedIndex = 0
		Me.tabQuote.Size = New System.Drawing.Size(601, 276)
		Me.tabQuote.TabIndex = 1
		'
		'tpgFind
		'
		Me.tpgFind.Controls.Add(Me.pnlSearchControls)
		Me.tpgFind.Controls.Add(Me.dgvSearchResults)
		Me.tpgFind.Location = New System.Drawing.Point(4, 22)
		Me.tpgFind.Name = "tpgFind"
		Me.tpgFind.Padding = New System.Windows.Forms.Padding(3)
		Me.tpgFind.Size = New System.Drawing.Size(593, 250)
		Me.tpgFind.TabIndex = 0
		Me.tpgFind.Text = "Find Quote"
		Me.tpgFind.UseVisualStyleBackColor = True
		'
		'pnlSearchControls
		'
		Me.pnlSearchControls.Controls.Add(Me.lblSelectAndShow)
		Me.pnlSearchControls.Controls.Add(Me.lblSearchInstructions)
		Me.pnlSearchControls.Controls.Add(Me.lblPartNumberLookup)
		Me.pnlSearchControls.Controls.Add(Me.lblRFQLookup)
		Me.pnlSearchControls.Controls.Add(Me.lblQuoteNumberLookup)
		Me.pnlSearchControls.Controls.Add(Me.btnSeach)
		Me.pnlSearchControls.Controls.Add(Me.txtRFQLookup)
		Me.pnlSearchControls.Controls.Add(Me.txtQuoteNumberLookup)
		Me.pnlSearchControls.Controls.Add(Me.txtPartNumberLookup)
		Me.pnlSearchControls.Dock = System.Windows.Forms.DockStyle.Top
		Me.pnlSearchControls.Location = New System.Drawing.Point(3, 3)
		Me.pnlSearchControls.Name = "pnlSearchControls"
		Me.pnlSearchControls.Size = New System.Drawing.Size(587, 112)
		Me.pnlSearchControls.TabIndex = 5
		'
		'lblSelectAndShow
		'
		Me.lblSelectAndShow.AutoSize = True
		Me.lblSelectAndShow.ForeColor = System.Drawing.SystemColors.ActiveCaption
		Me.lblSelectAndShow.Location = New System.Drawing.Point(6, 98)
		Me.lblSelectAndShow.Name = "lblSelectAndShow"
		Me.lblSelectAndShow.Size = New System.Drawing.Size(337, 13)
		Me.lblSelectAndShow.TabIndex = 9
		Me.lblSelectAndShow.Text = "To view quote details, click on a quote and click the Show Quote tab."
		'
		'lblSearchInstructions
		'
		Me.lblSearchInstructions.AutoSize = True
		Me.lblSearchInstructions.Location = New System.Drawing.Point(3, 0)
		Me.lblSearchInstructions.Name = "lblSearchInstructions"
		Me.lblSearchInstructions.Size = New System.Drawing.Size(590, 26)
		Me.lblSearchInstructions.TabIndex = 8
		Me.lblSearchInstructions.Text = resources.GetString("lblSearchInstructions.Text")
		'
		'lblPartNumberLookup
		'
		Me.lblPartNumberLookup.AutoSize = True
		Me.lblPartNumberLookup.Location = New System.Drawing.Point(45, 76)
		Me.lblPartNumberLookup.Name = "lblPartNumberLookup"
		Me.lblPartNumberLookup.Size = New System.Drawing.Size(69, 13)
		Me.lblPartNumberLookup.TabIndex = 7
		Me.lblPartNumberLookup.Text = "Part Number:"
		Me.lblPartNumberLookup.TextAlign = System.Drawing.ContentAlignment.MiddleRight
		'
		'lblRFQLookup
		'
		Me.lblRFQLookup.AutoSize = True
		Me.lblRFQLookup.Location = New System.Drawing.Point(82, 56)
		Me.lblRFQLookup.Name = "lblRFQLookup"
		Me.lblRFQLookup.Size = New System.Drawing.Size(32, 13)
		Me.lblRFQLookup.TabIndex = 6
		Me.lblRFQLookup.Text = "RFQ:"
		Me.lblRFQLookup.TextAlign = System.Drawing.ContentAlignment.MiddleRight
		'
		'lblQuoteNumberLookup
		'
		Me.lblQuoteNumberLookup.AutoSize = True
		Me.lblQuoteNumberLookup.Location = New System.Drawing.Point(35, 36)
		Me.lblQuoteNumberLookup.Name = "lblQuoteNumberLookup"
		Me.lblQuoteNumberLookup.Size = New System.Drawing.Size(79, 13)
		Me.lblQuoteNumberLookup.TabIndex = 5
		Me.lblQuoteNumberLookup.Text = "Quote Number:"
		Me.lblQuoteNumberLookup.TextAlign = System.Drawing.ContentAlignment.MiddleRight
		'
		'btnSeach
		'
		Me.btnSeach.Location = New System.Drawing.Point(463, 73)
		Me.btnSeach.Name = "btnSeach"
		Me.btnSeach.Size = New System.Drawing.Size(119, 23)
		Me.btnSeach.TabIndex = 2
		Me.btnSeach.Text = "Search for Quotes"
		Me.btnSeach.UseVisualStyleBackColor = True
		'
		'txtRFQLookup
		'
		Me.txtRFQLookup.Location = New System.Drawing.Point(120, 53)
		Me.txtRFQLookup.Name = "txtRFQLookup"
		Me.txtRFQLookup.Size = New System.Drawing.Size(100, 20)
		Me.txtRFQLookup.TabIndex = 3
		'
		'txtQuoteNumberLookup
		'
		Me.txtQuoteNumberLookup.Location = New System.Drawing.Point(120, 33)
		Me.txtQuoteNumberLookup.Name = "txtQuoteNumberLookup"
		Me.txtQuoteNumberLookup.Size = New System.Drawing.Size(100, 20)
		Me.txtQuoteNumberLookup.TabIndex = 0
		'
		'txtPartNumberLookup
		'
		Me.txtPartNumberLookup.Location = New System.Drawing.Point(120, 73)
		Me.txtPartNumberLookup.Name = "txtPartNumberLookup"
		Me.txtPartNumberLookup.Size = New System.Drawing.Size(100, 20)
		Me.txtPartNumberLookup.TabIndex = 4
		'
		'dgvSearchResults
		'
		Me.dgvSearchResults.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
		Me.dgvSearchResults.Dock = System.Windows.Forms.DockStyle.Bottom
		Me.dgvSearchResults.Location = New System.Drawing.Point(3, 124)
		Me.dgvSearchResults.Name = "dgvSearchResults"
		Me.dgvSearchResults.Size = New System.Drawing.Size(587, 123)
		Me.dgvSearchResults.TabIndex = 1
		'
		'tpgShow
		'
		Me.tpgShow.Controls.Add(Me.lblUnitPrice)
		Me.tpgShow.Controls.Add(Me.txtUnitPrice)
		Me.tpgShow.Controls.Add(Me.lblComputedPrice)
		Me.tpgShow.Controls.Add(Me.lblTimeMultiplier)
		Me.tpgShow.Controls.Add(Me.lblCuWeightMultiplier)
		Me.tpgShow.Controls.Add(Me.lblCuWeight)
		Me.tpgShow.Controls.Add(Me.lblCuPrice)
		Me.tpgShow.Controls.Add(Me.lblBoxPrice)
		Me.tpgShow.Controls.Add(Me.lblBoxSize)
		Me.tpgShow.Controls.Add(Me.lblVerifiedBy)
		Me.tpgShow.Controls.Add(Me.lblCreatedBy)
		Me.tpgShow.Controls.Add(Me.lblWireTime)
		Me.tpgShow.Controls.Add(Me.lblCuts)
		Me.tpgShow.Controls.Add(Me.lblTooling)
		Me.tpgShow.Controls.Add(Me.lblFormBoardCost)
		Me.tpgShow.Controls.Add(Me.lblLaborMinutes)
		Me.tpgShow.Controls.Add(Me.lblCutTime)
		Me.tpgShow.Controls.Add(Me.lblMinimum)
		Me.tpgShow.Controls.Add(Me.lblEAU)
		Me.tpgShow.Controls.Add(Me.lblContactName)
		Me.tpgShow.Controls.Add(Me.lblPartNumber)
		Me.tpgShow.Controls.Add(Me.lblLaborRate)
		Me.tpgShow.Controls.Add(Me.lblQuoteDate)
		Me.tpgShow.Controls.Add(Me.lblRFQ)
		Me.tpgShow.Controls.Add(Me.lblShipping)
		Me.tpgShow.Controls.Add(Me.lblQuoteNumber)
		Me.tpgShow.Controls.Add(Me.txtComputedPrice)
		Me.tpgShow.Controls.Add(Me.txtPartRevision)
		Me.tpgShow.Controls.Add(Me.txtBoxSize)
		Me.tpgShow.Controls.Add(Me.txtBoxPrice)
		Me.tpgShow.Controls.Add(Me.txtCuPrice)
		Me.tpgShow.Controls.Add(Me.txtCuWeight)
		Me.tpgShow.Controls.Add(Me.txtCuWeightMultiplier)
		Me.tpgShow.Controls.Add(Me.txtTimeMultiplier)
		Me.tpgShow.Controls.Add(Me.txtVerifiedBy)
		Me.tpgShow.Controls.Add(Me.txtFormBoardCost)
		Me.tpgShow.Controls.Add(Me.txtWireTime)
		Me.tpgShow.Controls.Add(Me.txtCutTime)
		Me.tpgShow.Controls.Add(Me.txtCuts)
		Me.tpgShow.Controls.Add(Me.txtCreatedBy)
		Me.tpgShow.Controls.Add(Me.txtContactName)
		Me.tpgShow.Controls.Add(Me.txtRFQ)
		Me.tpgShow.Controls.Add(Me.txtQuoteDate)
		Me.tpgShow.Controls.Add(Me.txtPartNumber)
		Me.tpgShow.Controls.Add(Me.txtMinimum)
		Me.tpgShow.Controls.Add(Me.txtLaborMinutes)
		Me.tpgShow.Controls.Add(Me.txtLaborRate)
		Me.tpgShow.Controls.Add(Me.txtTooling)
		Me.tpgShow.Controls.Add(Me.txtShipping)
		Me.tpgShow.Controls.Add(Me.txtEAU)
		Me.tpgShow.Controls.Add(Me.txtQuoteNumber)
		Me.tpgShow.Location = New System.Drawing.Point(4, 22)
		Me.tpgShow.Name = "tpgShow"
		Me.tpgShow.Padding = New System.Windows.Forms.Padding(3)
		Me.tpgShow.Size = New System.Drawing.Size(593, 250)
		Me.tpgShow.TabIndex = 1
		Me.tpgShow.Text = "Show Quote"
		Me.tpgShow.UseVisualStyleBackColor = True
		'
		'lblUnitPrice
		'
		Me.lblUnitPrice.AutoSize = True
		Me.lblUnitPrice.Location = New System.Drawing.Point(412, 191)
		Me.lblUnitPrice.Margin = New System.Windows.Forms.Padding(3, 0, 0, 0)
		Me.lblUnitPrice.Name = "lblUnitPrice"
		Me.lblUnitPrice.Size = New System.Drawing.Size(72, 13)
		Me.lblUnitPrice.TabIndex = 53
		Me.lblUnitPrice.Text = "XL Unit Price:"
		Me.lblUnitPrice.TextAlign = System.Drawing.ContentAlignment.MiddleRight
		'
		'txtUnitPrice
		'
		Me.txtUnitPrice.Location = New System.Drawing.Point(487, 188)
		Me.txtUnitPrice.Name = "txtUnitPrice"
		Me.txtUnitPrice.Size = New System.Drawing.Size(100, 20)
		Me.txtUnitPrice.TabIndex = 52
		'
		'lblComputedPrice
		'
		Me.lblComputedPrice.AutoSize = True
		Me.lblComputedPrice.Location = New System.Drawing.Point(399, 217)
		Me.lblComputedPrice.Margin = New System.Windows.Forms.Padding(3, 0, 0, 0)
		Me.lblComputedPrice.Name = "lblComputedPrice"
		Me.lblComputedPrice.Size = New System.Drawing.Size(85, 13)
		Me.lblComputedPrice.TabIndex = 51
		Me.lblComputedPrice.Text = "Computed Price:"
		Me.lblComputedPrice.TextAlign = System.Drawing.ContentAlignment.MiddleRight
		'
		'lblTimeMultiplier
		'
		Me.lblTimeMultiplier.AutoSize = True
		Me.lblTimeMultiplier.Location = New System.Drawing.Point(407, 142)
		Me.lblTimeMultiplier.Margin = New System.Windows.Forms.Padding(3, 0, 0, 0)
		Me.lblTimeMultiplier.Name = "lblTimeMultiplier"
		Me.lblTimeMultiplier.Size = New System.Drawing.Size(77, 13)
		Me.lblTimeMultiplier.TabIndex = 50
		Me.lblTimeMultiplier.Text = "Time Multiplier:"
		Me.lblTimeMultiplier.TextAlign = System.Drawing.ContentAlignment.MiddleRight
		'
		'lblCuWeightMultiplier
		'
		Me.lblCuWeightMultiplier.AutoSize = True
		Me.lblCuWeightMultiplier.Location = New System.Drawing.Point(383, 116)
		Me.lblCuWeightMultiplier.Margin = New System.Windows.Forms.Padding(3, 0, 0, 0)
		Me.lblCuWeightMultiplier.Name = "lblCuWeightMultiplier"
		Me.lblCuWeightMultiplier.Size = New System.Drawing.Size(104, 13)
		Me.lblCuWeightMultiplier.TabIndex = 49
		Me.lblCuWeightMultiplier.Text = "Cu Weight Multiplier:"
		Me.lblCuWeightMultiplier.TextAlign = System.Drawing.ContentAlignment.MiddleRight
		'
		'lblCuWeight
		'
		Me.lblCuWeight.AutoSize = True
		Me.lblCuWeight.Location = New System.Drawing.Point(217, 217)
		Me.lblCuWeight.Margin = New System.Windows.Forms.Padding(3, 0, 0, 0)
		Me.lblCuWeight.Name = "lblCuWeight"
		Me.lblCuWeight.Size = New System.Drawing.Size(60, 13)
		Me.lblCuWeight.TabIndex = 48
		Me.lblCuWeight.Text = "Cu Weight:"
		Me.lblCuWeight.TextAlign = System.Drawing.ContentAlignment.MiddleRight
		'
		'lblCuPrice
		'
		Me.lblCuPrice.AutoSize = True
		Me.lblCuPrice.Location = New System.Drawing.Point(434, 38)
		Me.lblCuPrice.Margin = New System.Windows.Forms.Padding(3, 0, 0, 0)
		Me.lblCuPrice.Name = "lblCuPrice"
		Me.lblCuPrice.Size = New System.Drawing.Size(50, 13)
		Me.lblCuPrice.TabIndex = 47
		Me.lblCuPrice.Text = "Cu Price:"
		Me.lblCuPrice.TextAlign = System.Drawing.ContentAlignment.MiddleRight
		'
		'lblBoxPrice
		'
		Me.lblBoxPrice.AutoSize = True
		Me.lblBoxPrice.Location = New System.Drawing.Point(222, 165)
		Me.lblBoxPrice.Margin = New System.Windows.Forms.Padding(3, 0, 0, 0)
		Me.lblBoxPrice.Name = "lblBoxPrice"
		Me.lblBoxPrice.Size = New System.Drawing.Size(55, 13)
		Me.lblBoxPrice.TabIndex = 46
		Me.lblBoxPrice.Text = "Box Price:"
		Me.lblBoxPrice.TextAlign = System.Drawing.ContentAlignment.MiddleRight
		'
		'lblBoxSize
		'
		Me.lblBoxSize.AutoSize = True
		Me.lblBoxSize.Location = New System.Drawing.Point(226, 139)
		Me.lblBoxSize.Margin = New System.Windows.Forms.Padding(3, 0, 0, 0)
		Me.lblBoxSize.Name = "lblBoxSize"
		Me.lblBoxSize.Size = New System.Drawing.Size(51, 13)
		Me.lblBoxSize.TabIndex = 45
		Me.lblBoxSize.Text = "Box Size:"
		Me.lblBoxSize.TextAlign = System.Drawing.ContentAlignment.MiddleRight
		'
		'lblVerifiedBy
		'
		Me.lblVerifiedBy.AutoSize = True
		Me.lblVerifiedBy.Location = New System.Drawing.Point(20, 191)
		Me.lblVerifiedBy.Margin = New System.Windows.Forms.Padding(3, 0, 0, 0)
		Me.lblVerifiedBy.Name = "lblVerifiedBy"
		Me.lblVerifiedBy.Size = New System.Drawing.Size(60, 13)
		Me.lblVerifiedBy.TabIndex = 44
		Me.lblVerifiedBy.Text = "Verified By:"
		Me.lblVerifiedBy.TextAlign = System.Drawing.ContentAlignment.MiddleRight
		'
		'lblCreatedBy
		'
		Me.lblCreatedBy.AutoSize = True
		Me.lblCreatedBy.Location = New System.Drawing.Point(18, 165)
		Me.lblCreatedBy.Margin = New System.Windows.Forms.Padding(3, 0, 0, 0)
		Me.lblCreatedBy.Name = "lblCreatedBy"
		Me.lblCreatedBy.Size = New System.Drawing.Size(62, 13)
		Me.lblCreatedBy.TabIndex = 43
		Me.lblCreatedBy.Text = "Created By:"
		Me.lblCreatedBy.TextAlign = System.Drawing.ContentAlignment.MiddleRight
		'
		'lblWireTime
		'
		Me.lblWireTime.AutoSize = True
		Me.lblWireTime.Location = New System.Drawing.Point(426, 64)
		Me.lblWireTime.Margin = New System.Windows.Forms.Padding(3, 0, 0, 0)
		Me.lblWireTime.Name = "lblWireTime"
		Me.lblWireTime.Size = New System.Drawing.Size(58, 13)
		Me.lblWireTime.TabIndex = 40
		Me.lblWireTime.Text = "Wire Time:"
		Me.lblWireTime.TextAlign = System.Drawing.ContentAlignment.MiddleRight
		'
		'lblCuts
		'
		Me.lblCuts.AutoSize = True
		Me.lblCuts.Location = New System.Drawing.Point(246, 191)
		Me.lblCuts.Margin = New System.Windows.Forms.Padding(3, 0, 0, 0)
		Me.lblCuts.Name = "lblCuts"
		Me.lblCuts.Size = New System.Drawing.Size(31, 13)
		Me.lblCuts.TabIndex = 39
		Me.lblCuts.Text = "Cuts:"
		Me.lblCuts.TextAlign = System.Drawing.ContentAlignment.MiddleRight
		'
		'lblTooling
		'
		Me.lblTooling.AutoSize = True
		Me.lblTooling.Location = New System.Drawing.Point(232, 61)
		Me.lblTooling.Margin = New System.Windows.Forms.Padding(3, 0, 0, 0)
		Me.lblTooling.Name = "lblTooling"
		Me.lblTooling.Size = New System.Drawing.Size(45, 13)
		Me.lblTooling.TabIndex = 38
		Me.lblTooling.Text = "Tooling:"
		Me.lblTooling.TextAlign = System.Drawing.ContentAlignment.MiddleRight
		'
		'lblFormBoardCost
		'
		Me.lblFormBoardCost.AutoSize = True
		Me.lblFormBoardCost.Location = New System.Drawing.Point(189, 113)
		Me.lblFormBoardCost.Margin = New System.Windows.Forms.Padding(3, 0, 0, 0)
		Me.lblFormBoardCost.Name = "lblFormBoardCost"
		Me.lblFormBoardCost.Size = New System.Drawing.Size(88, 13)
		Me.lblFormBoardCost.TabIndex = 37
		Me.lblFormBoardCost.Text = "Form Board Cost:"
		Me.lblFormBoardCost.TextAlign = System.Drawing.ContentAlignment.MiddleRight
		'
		'lblLaborMinutes
		'
		Me.lblLaborMinutes.AutoSize = True
		Me.lblLaborMinutes.Location = New System.Drawing.Point(203, 35)
		Me.lblLaborMinutes.Margin = New System.Windows.Forms.Padding(3, 0, 0, 0)
		Me.lblLaborMinutes.Name = "lblLaborMinutes"
		Me.lblLaborMinutes.Size = New System.Drawing.Size(74, 13)
		Me.lblLaborMinutes.TabIndex = 36
		Me.lblLaborMinutes.Text = "LaborMinutes:"
		Me.lblLaborMinutes.TextAlign = System.Drawing.ContentAlignment.MiddleRight
		'
		'lblCutTime
		'
		Me.lblCutTime.AutoSize = True
		Me.lblCutTime.Location = New System.Drawing.Point(432, 90)
		Me.lblCutTime.Margin = New System.Windows.Forms.Padding(3, 0, 0, 0)
		Me.lblCutTime.Name = "lblCutTime"
		Me.lblCutTime.Size = New System.Drawing.Size(52, 13)
		Me.lblCutTime.TabIndex = 35
		Me.lblCutTime.Text = "Cut Time:"
		Me.lblCutTime.TextAlign = System.Drawing.ContentAlignment.MiddleRight
		'
		'lblMinimum
		'
		Me.lblMinimum.AutoSize = True
		Me.lblMinimum.Location = New System.Drawing.Point(226, 9)
		Me.lblMinimum.Margin = New System.Windows.Forms.Padding(3, 0, 0, 0)
		Me.lblMinimum.Name = "lblMinimum"
		Me.lblMinimum.Size = New System.Drawing.Size(51, 13)
		Me.lblMinimum.TabIndex = 34
		Me.lblMinimum.Text = "Minimum:"
		Me.lblMinimum.TextAlign = System.Drawing.ContentAlignment.MiddleRight
		'
		'lblEAU
		'
		Me.lblEAU.AutoSize = True
		Me.lblEAU.Location = New System.Drawing.Point(48, 113)
		Me.lblEAU.Margin = New System.Windows.Forms.Padding(3, 0, 0, 0)
		Me.lblEAU.Name = "lblEAU"
		Me.lblEAU.Size = New System.Drawing.Size(32, 13)
		Me.lblEAU.TabIndex = 33
		Me.lblEAU.Text = "EAU:"
		Me.lblEAU.TextAlign = System.Drawing.ContentAlignment.MiddleRight
		'
		'lblContactName
		'
		Me.lblContactName.AutoSize = True
		Me.lblContactName.Location = New System.Drawing.Point(2, 139)
		Me.lblContactName.Margin = New System.Windows.Forms.Padding(3, 0, 0, 0)
		Me.lblContactName.Name = "lblContactName"
		Me.lblContactName.Size = New System.Drawing.Size(78, 13)
		Me.lblContactName.TabIndex = 32
		Me.lblContactName.Text = "Contact Name:"
		Me.lblContactName.TextAlign = System.Drawing.ContentAlignment.MiddleRight
		'
		'lblPartNumber
		'
		Me.lblPartNumber.AutoSize = True
		Me.lblPartNumber.Location = New System.Drawing.Point(11, 87)
		Me.lblPartNumber.Margin = New System.Windows.Forms.Padding(3, 0, 0, 0)
		Me.lblPartNumber.Name = "lblPartNumber"
		Me.lblPartNumber.Size = New System.Drawing.Size(69, 13)
		Me.lblPartNumber.TabIndex = 31
		Me.lblPartNumber.Text = "Part Number:"
		Me.lblPartNumber.TextAlign = System.Drawing.ContentAlignment.MiddleRight
		'
		'lblLaborRate
		'
		Me.lblLaborRate.AutoSize = True
		Me.lblLaborRate.Location = New System.Drawing.Point(421, 12)
		Me.lblLaborRate.Margin = New System.Windows.Forms.Padding(3, 0, 0, 0)
		Me.lblLaborRate.Name = "lblLaborRate"
		Me.lblLaborRate.Size = New System.Drawing.Size(63, 13)
		Me.lblLaborRate.TabIndex = 30
		Me.lblLaborRate.Text = "Labor Rate:"
		Me.lblLaborRate.TextAlign = System.Drawing.ContentAlignment.MiddleRight
		'
		'lblQuoteDate
		'
		Me.lblQuoteDate.AutoSize = True
		Me.lblQuoteDate.Location = New System.Drawing.Point(15, 61)
		Me.lblQuoteDate.Margin = New System.Windows.Forms.Padding(3, 0, 0, 0)
		Me.lblQuoteDate.Name = "lblQuoteDate"
		Me.lblQuoteDate.Size = New System.Drawing.Size(65, 13)
		Me.lblQuoteDate.TabIndex = 29
		Me.lblQuoteDate.Text = "Quote Date:"
		Me.lblQuoteDate.TextAlign = System.Drawing.ContentAlignment.MiddleRight
		'
		'lblRFQ
		'
		Me.lblRFQ.AutoSize = True
		Me.lblRFQ.Location = New System.Drawing.Point(48, 35)
		Me.lblRFQ.Margin = New System.Windows.Forms.Padding(3, 0, 0, 0)
		Me.lblRFQ.Name = "lblRFQ"
		Me.lblRFQ.Size = New System.Drawing.Size(32, 13)
		Me.lblRFQ.TabIndex = 28
		Me.lblRFQ.Text = "RFQ:"
		Me.lblRFQ.TextAlign = System.Drawing.ContentAlignment.MiddleRight
		'
		'lblShipping
		'
		Me.lblShipping.AutoSize = True
		Me.lblShipping.Location = New System.Drawing.Point(226, 87)
		Me.lblShipping.Margin = New System.Windows.Forms.Padding(3, 0, 0, 0)
		Me.lblShipping.Name = "lblShipping"
		Me.lblShipping.Size = New System.Drawing.Size(51, 13)
		Me.lblShipping.TabIndex = 27
		Me.lblShipping.Text = "Shipping:"
		Me.lblShipping.TextAlign = System.Drawing.ContentAlignment.MiddleRight
		'
		'lblQuoteNumber
		'
		Me.lblQuoteNumber.AutoSize = True
		Me.lblQuoteNumber.Location = New System.Drawing.Point(1, 9)
		Me.lblQuoteNumber.Margin = New System.Windows.Forms.Padding(3, 0, 0, 0)
		Me.lblQuoteNumber.Name = "lblQuoteNumber"
		Me.lblQuoteNumber.Size = New System.Drawing.Size(79, 13)
		Me.lblQuoteNumber.TabIndex = 26
		Me.lblQuoteNumber.Text = "Quote Number:"
		Me.lblQuoteNumber.TextAlign = System.Drawing.ContentAlignment.MiddleRight
		'
		'txtComputedPrice
		'
		Me.txtComputedPrice.Location = New System.Drawing.Point(487, 214)
		Me.txtComputedPrice.Name = "txtComputedPrice"
		Me.txtComputedPrice.Size = New System.Drawing.Size(100, 20)
		Me.txtComputedPrice.TabIndex = 25
		'
		'txtPartRevision
		'
		Me.txtPartRevision.Location = New System.Drawing.Point(180, 84)
		Me.txtPartRevision.Name = "txtPartRevision"
		Me.txtPartRevision.Size = New System.Drawing.Size(40, 20)
		Me.txtPartRevision.TabIndex = 24
		'
		'txtBoxSize
		'
		Me.txtBoxSize.Location = New System.Drawing.Point(280, 136)
		Me.txtBoxSize.Name = "txtBoxSize"
		Me.txtBoxSize.Size = New System.Drawing.Size(100, 20)
		Me.txtBoxSize.TabIndex = 22
		'
		'txtBoxPrice
		'
		Me.txtBoxPrice.Location = New System.Drawing.Point(280, 162)
		Me.txtBoxPrice.Name = "txtBoxPrice"
		Me.txtBoxPrice.Size = New System.Drawing.Size(100, 20)
		Me.txtBoxPrice.TabIndex = 21
		'
		'txtCuPrice
		'
		Me.txtCuPrice.Location = New System.Drawing.Point(487, 35)
		Me.txtCuPrice.Name = "txtCuPrice"
		Me.txtCuPrice.Size = New System.Drawing.Size(100, 20)
		Me.txtCuPrice.TabIndex = 20
		'
		'txtCuWeight
		'
		Me.txtCuWeight.Location = New System.Drawing.Point(280, 214)
		Me.txtCuWeight.Name = "txtCuWeight"
		Me.txtCuWeight.Size = New System.Drawing.Size(100, 20)
		Me.txtCuWeight.TabIndex = 19
		'
		'txtCuWeightMultiplier
		'
		Me.txtCuWeightMultiplier.Location = New System.Drawing.Point(487, 113)
		Me.txtCuWeightMultiplier.Name = "txtCuWeightMultiplier"
		Me.txtCuWeightMultiplier.Size = New System.Drawing.Size(100, 20)
		Me.txtCuWeightMultiplier.TabIndex = 18
		'
		'txtTimeMultiplier
		'
		Me.txtTimeMultiplier.Location = New System.Drawing.Point(487, 139)
		Me.txtTimeMultiplier.Name = "txtTimeMultiplier"
		Me.txtTimeMultiplier.Size = New System.Drawing.Size(100, 20)
		Me.txtTimeMultiplier.TabIndex = 17
		'
		'txtVerifiedBy
		'
		Me.txtVerifiedBy.Location = New System.Drawing.Point(83, 188)
		Me.txtVerifiedBy.Name = "txtVerifiedBy"
		Me.txtVerifiedBy.Size = New System.Drawing.Size(100, 20)
		Me.txtVerifiedBy.TabIndex = 16
		'
		'txtFormBoardCost
		'
		Me.txtFormBoardCost.Location = New System.Drawing.Point(280, 110)
		Me.txtFormBoardCost.Name = "txtFormBoardCost"
		Me.txtFormBoardCost.Size = New System.Drawing.Size(100, 20)
		Me.txtFormBoardCost.TabIndex = 15
		'
		'txtWireTime
		'
		Me.txtWireTime.Location = New System.Drawing.Point(487, 61)
		Me.txtWireTime.Name = "txtWireTime"
		Me.txtWireTime.Size = New System.Drawing.Size(100, 20)
		Me.txtWireTime.TabIndex = 14
		'
		'txtCutTime
		'
		Me.txtCutTime.Location = New System.Drawing.Point(487, 87)
		Me.txtCutTime.Name = "txtCutTime"
		Me.txtCutTime.Size = New System.Drawing.Size(100, 20)
		Me.txtCutTime.TabIndex = 13
		'
		'txtCuts
		'
		Me.txtCuts.Location = New System.Drawing.Point(280, 188)
		Me.txtCuts.Name = "txtCuts"
		Me.txtCuts.Size = New System.Drawing.Size(100, 20)
		Me.txtCuts.TabIndex = 12
		'
		'txtCreatedBy
		'
		Me.txtCreatedBy.Location = New System.Drawing.Point(83, 162)
		Me.txtCreatedBy.Name = "txtCreatedBy"
		Me.txtCreatedBy.Size = New System.Drawing.Size(100, 20)
		Me.txtCreatedBy.TabIndex = 11
		'
		'txtContactName
		'
		Me.txtContactName.Location = New System.Drawing.Point(83, 136)
		Me.txtContactName.Name = "txtContactName"
		Me.txtContactName.Size = New System.Drawing.Size(100, 20)
		Me.txtContactName.TabIndex = 10
		'
		'txtRFQ
		'
		Me.txtRFQ.Location = New System.Drawing.Point(83, 32)
		Me.txtRFQ.Name = "txtRFQ"
		Me.txtRFQ.Size = New System.Drawing.Size(100, 20)
		Me.txtRFQ.TabIndex = 9
		'
		'txtQuoteDate
		'
		Me.txtQuoteDate.Location = New System.Drawing.Point(83, 58)
		Me.txtQuoteDate.Name = "txtQuoteDate"
		Me.txtQuoteDate.Size = New System.Drawing.Size(100, 20)
		Me.txtQuoteDate.TabIndex = 8
		'
		'txtPartNumber
		'
		Me.txtPartNumber.Location = New System.Drawing.Point(83, 84)
		Me.txtPartNumber.Name = "txtPartNumber"
		Me.txtPartNumber.Size = New System.Drawing.Size(100, 20)
		Me.txtPartNumber.TabIndex = 7
		'
		'txtMinimum
		'
		Me.txtMinimum.Location = New System.Drawing.Point(280, 6)
		Me.txtMinimum.Name = "txtMinimum"
		Me.txtMinimum.Size = New System.Drawing.Size(100, 20)
		Me.txtMinimum.TabIndex = 6
		'
		'txtLaborMinutes
		'
		Me.txtLaborMinutes.Location = New System.Drawing.Point(280, 32)
		Me.txtLaborMinutes.Name = "txtLaborMinutes"
		Me.txtLaborMinutes.Size = New System.Drawing.Size(100, 20)
		Me.txtLaborMinutes.TabIndex = 5
		'
		'txtLaborRate
		'
		Me.txtLaborRate.Location = New System.Drawing.Point(487, 9)
		Me.txtLaborRate.Name = "txtLaborRate"
		Me.txtLaborRate.Size = New System.Drawing.Size(100, 20)
		Me.txtLaborRate.TabIndex = 4
		'
		'txtTooling
		'
		Me.txtTooling.Location = New System.Drawing.Point(280, 58)
		Me.txtTooling.Name = "txtTooling"
		Me.txtTooling.Size = New System.Drawing.Size(100, 20)
		Me.txtTooling.TabIndex = 3
		'
		'txtShipping
		'
		Me.txtShipping.Location = New System.Drawing.Point(280, 84)
		Me.txtShipping.Name = "txtShipping"
		Me.txtShipping.Size = New System.Drawing.Size(100, 20)
		Me.txtShipping.TabIndex = 2
		'
		'txtEAU
		'
		Me.txtEAU.Location = New System.Drawing.Point(83, 110)
		Me.txtEAU.Name = "txtEAU"
		Me.txtEAU.Size = New System.Drawing.Size(100, 20)
		Me.txtEAU.TabIndex = 1
		'
		'txtQuoteNumber
		'
		Me.txtQuoteNumber.Location = New System.Drawing.Point(83, 6)
		Me.txtQuoteNumber.Name = "txtQuoteNumber"
		Me.txtQuoteNumber.Size = New System.Drawing.Size(100, 20)
		Me.txtQuoteNumber.TabIndex = 0
		'
		'frmQuoteMain
		'
		Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
		Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
		Me.ClientSize = New System.Drawing.Size(610, 348)
		Me.Controls.Add(Me.tabQuote)
		Me.Controls.Add(Me.mnuQuote)
		Me.MainMenuStrip = Me.mnuQuote
		Me.Name = "frmQuoteMain"
		Me.Text = "WHQ Quote Maker"
		Me.mnuQuote.ResumeLayout(False)
		Me.mnuQuote.PerformLayout()
		Me.tabQuote.ResumeLayout(False)
		Me.tpgFind.ResumeLayout(False)
		Me.pnlSearchControls.ResumeLayout(False)
		Me.pnlSearchControls.PerformLayout()
		CType(Me.dgvSearchResults, System.ComponentModel.ISupportInitialize).EndInit()
		Me.tpgShow.ResumeLayout(False)
		Me.tpgShow.PerformLayout()
		Me.ResumeLayout(False)
		Me.PerformLayout()

	End Sub
	Friend WithEvents mnuQuote As System.Windows.Forms.MenuStrip
	Friend WithEvents mniImport As System.Windows.Forms.ToolStripMenuItem
	Friend WithEvents mniOpen As System.Windows.Forms.ToolStripMenuItem
	Friend WithEvents tabQuote As System.Windows.Forms.TabControl
	Friend WithEvents tpgFind As System.Windows.Forms.TabPage
	Friend WithEvents tpgShow As System.Windows.Forms.TabPage
	Friend WithEvents dgvSearchResults As System.Windows.Forms.DataGridView
	Friend WithEvents pnlSearchControls As System.Windows.Forms.Panel
	Friend WithEvents btnSeach As System.Windows.Forms.Button
	Friend WithEvents txtPartNumberLookup As System.Windows.Forms.TextBox
	Friend WithEvents txtRFQLookup As System.Windows.Forms.TextBox
	Friend WithEvents txtQuoteNumberLookup As System.Windows.Forms.TextBox
	Friend WithEvents txtRFQ As System.Windows.Forms.TextBox
	Friend WithEvents txtQuoteDate As System.Windows.Forms.TextBox
	Friend WithEvents txtPartNumber As System.Windows.Forms.TextBox
	Friend WithEvents txtMinimum As System.Windows.Forms.TextBox
	Friend WithEvents txtLaborMinutes As System.Windows.Forms.TextBox
	Friend WithEvents txtLaborRate As System.Windows.Forms.TextBox
	Friend WithEvents txtTooling As System.Windows.Forms.TextBox
	Friend WithEvents txtShipping As System.Windows.Forms.TextBox
	Friend WithEvents txtEAU As System.Windows.Forms.TextBox
	Friend WithEvents txtQuoteNumber As System.Windows.Forms.TextBox
	Friend WithEvents txtPartRevision As System.Windows.Forms.TextBox
	Friend WithEvents txtBoxSize As System.Windows.Forms.TextBox
	Friend WithEvents txtBoxPrice As System.Windows.Forms.TextBox
	Friend WithEvents txtCuPrice As System.Windows.Forms.TextBox
	Friend WithEvents txtCuWeight As System.Windows.Forms.TextBox
	Friend WithEvents txtCuWeightMultiplier As System.Windows.Forms.TextBox
	Friend WithEvents txtTimeMultiplier As System.Windows.Forms.TextBox
	Friend WithEvents txtVerifiedBy As System.Windows.Forms.TextBox
	Friend WithEvents txtFormBoardCost As System.Windows.Forms.TextBox
	Friend WithEvents txtWireTime As System.Windows.Forms.TextBox
	Friend WithEvents txtCutTime As System.Windows.Forms.TextBox
	Friend WithEvents txtCuts As System.Windows.Forms.TextBox
	Friend WithEvents txtCreatedBy As System.Windows.Forms.TextBox
	Friend WithEvents txtContactName As System.Windows.Forms.TextBox
	Friend WithEvents txtComputedPrice As System.Windows.Forms.TextBox
	Friend WithEvents lblPartNumberLookup As System.Windows.Forms.Label
	Friend WithEvents lblRFQLookup As System.Windows.Forms.Label
	Friend WithEvents lblQuoteNumberLookup As System.Windows.Forms.Label
	Friend WithEvents lblSearchInstructions As System.Windows.Forms.Label
	Friend WithEvents lblSelectAndShow As System.Windows.Forms.Label
	Friend WithEvents lblQuoteNumber As System.Windows.Forms.Label
	Friend WithEvents lblComputedPrice As System.Windows.Forms.Label
	Friend WithEvents lblTimeMultiplier As System.Windows.Forms.Label
	Friend WithEvents lblCuWeightMultiplier As System.Windows.Forms.Label
	Friend WithEvents lblCuWeight As System.Windows.Forms.Label
	Friend WithEvents lblCuPrice As System.Windows.Forms.Label
	Friend WithEvents lblBoxPrice As System.Windows.Forms.Label
	Friend WithEvents lblBoxSize As System.Windows.Forms.Label
	Friend WithEvents lblVerifiedBy As System.Windows.Forms.Label
	Friend WithEvents lblCreatedBy As System.Windows.Forms.Label
	Friend WithEvents lblWireTime As System.Windows.Forms.Label
	Friend WithEvents lblCuts As System.Windows.Forms.Label
	Friend WithEvents lblTooling As System.Windows.Forms.Label
	Friend WithEvents lblFormBoardCost As System.Windows.Forms.Label
	Friend WithEvents lblLaborMinutes As System.Windows.Forms.Label
	Friend WithEvents lblCutTime As System.Windows.Forms.Label
	Friend WithEvents lblMinimum As System.Windows.Forms.Label
	Friend WithEvents lblEAU As System.Windows.Forms.Label
	Friend WithEvents lblContactName As System.Windows.Forms.Label
	Friend WithEvents lblPartNumber As System.Windows.Forms.Label
	Friend WithEvents lblLaborRate As System.Windows.Forms.Label
	Friend WithEvents lblQuoteDate As System.Windows.Forms.Label
	Friend WithEvents lblRFQ As System.Windows.Forms.Label
	Friend WithEvents lblShipping As System.Windows.Forms.Label
	Friend WithEvents lblUnitPrice As System.Windows.Forms.Label
	Friend WithEvents txtUnitPrice As System.Windows.Forms.TextBox
End Class
