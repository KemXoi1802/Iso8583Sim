using System;
using System.CodeDom.Compiler;
using System.Collections;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Runtime.Serialization;
using System.Text;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace Iso8583Simu
{
  [HelpKeyword("vs.data.DataSet")]
  [XmlRoot("DS_EMV_INFO")]
  [DesignerCategory("code")]
  [ToolboxItem(true)]
  [XmlSchemaProvider("GetTypedDataSetSchema")]
  [Serializable]
  public class DS_EMV_INFO : DataSet
  {
    private SchemaSerializationMode _schemaSerializationMode = SchemaSerializationMode.IncludeSchema;
    private DS_EMV_INFO.EMV_TAGSDataTable tableEMV_TAGS;
    private DS_EMV_INFO.TAG_BITSDataTable tableTAG_BITS;
    private DataRelation relationEMV_TAGS_TAG_BITS;

    [DebuggerNonUserCode]
    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    public DS_EMV_INFO()
    {
      this.BeginInit();
      this.InitClass();
      CollectionChangeEventHandler changeEventHandler = new CollectionChangeEventHandler(this.SchemaChanged);
      base.Tables.CollectionChanged += changeEventHandler;
      base.Relations.CollectionChanged += changeEventHandler;
      this.EndInit();
    }

    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    [DebuggerNonUserCode]
    protected DS_EMV_INFO(SerializationInfo info, StreamingContext context)
      : base(info, context, false)
    {
      if (this.IsBinarySerialized(info, context))
      {
        this.InitVars(false);
        CollectionChangeEventHandler changeEventHandler = new CollectionChangeEventHandler(this.SchemaChanged);
        this.Tables.CollectionChanged += changeEventHandler;
        this.Relations.CollectionChanged += changeEventHandler;
      }
      else
      {
        string s = (string) info.GetValue("XmlSchema", typeof (string));
        if (this.DetermineSchemaSerializationMode(info, context) == SchemaSerializationMode.IncludeSchema)
        {
          DataSet dataSet = new DataSet();
          dataSet.ReadXmlSchema((XmlReader) new XmlTextReader((TextReader) new StringReader(s)));
          if (dataSet.Tables[nameof (EMV_TAGS)] != null)
            base.Tables.Add((DataTable) new DS_EMV_INFO.EMV_TAGSDataTable(dataSet.Tables[nameof (EMV_TAGS)]));
          if (dataSet.Tables[nameof (TAG_BITS)] != null)
            base.Tables.Add((DataTable) new DS_EMV_INFO.TAG_BITSDataTable(dataSet.Tables[nameof (TAG_BITS)]));
          this.DataSetName = dataSet.DataSetName;
          this.Prefix = dataSet.Prefix;
          this.Namespace = dataSet.Namespace;
          this.Locale = dataSet.Locale;
          this.CaseSensitive = dataSet.CaseSensitive;
          this.EnforceConstraints = dataSet.EnforceConstraints;
          this.Merge(dataSet, false, MissingSchemaAction.Add);
          this.InitVars();
        }
        else
          this.ReadXmlSchema((XmlReader) new XmlTextReader((TextReader) new StringReader(s)));
        this.GetSerializationData(info, context);
        CollectionChangeEventHandler changeEventHandler = new CollectionChangeEventHandler(this.SchemaChanged);
        base.Tables.CollectionChanged += changeEventHandler;
        this.Relations.CollectionChanged += changeEventHandler;
      }
    }

    [DebuggerNonUserCode]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    [Browsable(false)]
    public DS_EMV_INFO.EMV_TAGSDataTable EMV_TAGS
    {
      get
      {
        return this.tableEMV_TAGS;
      }
    }

    [DebuggerNonUserCode]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    [Browsable(false)]
    public DS_EMV_INFO.TAG_BITSDataTable TAG_BITS
    {
      get
      {
        return this.tableTAG_BITS;
      }
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    [Browsable(true)]
    [DebuggerNonUserCode]
    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    public override SchemaSerializationMode SchemaSerializationMode
    {
      get
      {
        return this._schemaSerializationMode;
      }
      set
      {
        this._schemaSerializationMode = value;
      }
    }

    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [DebuggerNonUserCode]
    public new DataTableCollection Tables
    {
      get
      {
        return base.Tables;
      }
    }

    [DebuggerNonUserCode]
    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public new DataRelationCollection Relations
    {
      get
      {
        return base.Relations;
      }
    }

    [DebuggerNonUserCode]
    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    protected override void InitializeDerivedDataSet()
    {
      this.BeginInit();
      this.InitClass();
      this.EndInit();
    }

    [DebuggerNonUserCode]
    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    public override DataSet Clone()
    {
      DS_EMV_INFO dsEmvInfo = (DS_EMV_INFO) base.Clone();
      dsEmvInfo.InitVars();
      dsEmvInfo.SchemaSerializationMode = this.SchemaSerializationMode;
      return (DataSet) dsEmvInfo;
    }

    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    [DebuggerNonUserCode]
    protected override bool ShouldSerializeTables()
    {
      return false;
    }

    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    [DebuggerNonUserCode]
    protected override bool ShouldSerializeRelations()
    {
      return false;
    }

    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    [DebuggerNonUserCode]
    protected override void ReadXmlSerializable(XmlReader reader)
    {
      if (this.DetermineSchemaSerializationMode(reader) == SchemaSerializationMode.IncludeSchema)
      {
        this.Reset();
        DataSet dataSet = new DataSet();
        int num = (int) dataSet.ReadXml(reader);
        if (dataSet.Tables["EMV_TAGS"] != null)
          base.Tables.Add((DataTable) new DS_EMV_INFO.EMV_TAGSDataTable(dataSet.Tables["EMV_TAGS"]));
        if (dataSet.Tables["TAG_BITS"] != null)
          base.Tables.Add((DataTable) new DS_EMV_INFO.TAG_BITSDataTable(dataSet.Tables["TAG_BITS"]));
        this.DataSetName = dataSet.DataSetName;
        this.Prefix = dataSet.Prefix;
        this.Namespace = dataSet.Namespace;
        this.Locale = dataSet.Locale;
        this.CaseSensitive = dataSet.CaseSensitive;
        this.EnforceConstraints = dataSet.EnforceConstraints;
        this.Merge(dataSet, false, MissingSchemaAction.Add);
        this.InitVars();
      }
      else
      {
        int num = (int) this.ReadXml(reader);
        this.InitVars();
      }
    }

    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    [DebuggerNonUserCode]
    protected override XmlSchema GetSchemaSerializable()
    {
      MemoryStream memoryStream = new MemoryStream();
      this.WriteXmlSchema((XmlWriter) new XmlTextWriter((Stream) memoryStream, (Encoding) null));
      memoryStream.Position = 0L;
      return XmlSchema.Read((XmlReader) new XmlTextReader((Stream) memoryStream), (ValidationEventHandler) null);
    }

    [DebuggerNonUserCode]
    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    internal void InitVars()
    {
      this.InitVars(true);
    }

    [DebuggerNonUserCode]
    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    internal void InitVars(bool initTable)
    {
      this.tableEMV_TAGS = (DS_EMV_INFO.EMV_TAGSDataTable) base.Tables["EMV_TAGS"];
      if (initTable && this.tableEMV_TAGS != null)
        this.tableEMV_TAGS.InitVars();
      this.tableTAG_BITS = (DS_EMV_INFO.TAG_BITSDataTable) base.Tables["TAG_BITS"];
      if (initTable && this.tableTAG_BITS != null)
        this.tableTAG_BITS.InitVars();
      this.relationEMV_TAGS_TAG_BITS = this.Relations["EMV_TAGS_TAG_BITS"];
    }

    [DebuggerNonUserCode]
    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    private void InitClass()
    {
      this.DataSetName = nameof (DS_EMV_INFO);
      this.Prefix = "";
      this.Namespace = "http://tempuri.org/DS_EMV_INFO.xsd";
      this.EnforceConstraints = true;
      this.SchemaSerializationMode = SchemaSerializationMode.IncludeSchema;
      this.tableEMV_TAGS = new DS_EMV_INFO.EMV_TAGSDataTable();
      base.Tables.Add((DataTable) this.tableEMV_TAGS);
      this.tableTAG_BITS = new DS_EMV_INFO.TAG_BITSDataTable();
      base.Tables.Add((DataTable) this.tableTAG_BITS);
      this.relationEMV_TAGS_TAG_BITS = new DataRelation("EMV_TAGS_TAG_BITS", new DataColumn[1]
      {
        this.tableEMV_TAGS.TAGColumn
      }, new DataColumn[1]{ this.tableTAG_BITS.TAGColumn }, false);
      this.Relations.Add(this.relationEMV_TAGS_TAG_BITS);
    }

    [DebuggerNonUserCode]
    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    private bool ShouldSerializeEMV_TAGS()
    {
      return false;
    }

    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    [DebuggerNonUserCode]
    private bool ShouldSerializeTAG_BITS()
    {
      return false;
    }

    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    [DebuggerNonUserCode]
    private void SchemaChanged(object sender, CollectionChangeEventArgs e)
    {
      if (e.Action != CollectionChangeAction.Remove)
        return;
      this.InitVars();
    }

    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    [DebuggerNonUserCode]
    public static XmlSchemaComplexType GetTypedDataSetSchema(XmlSchemaSet xs)
    {
      DS_EMV_INFO dsEmvInfo = new DS_EMV_INFO();
      XmlSchemaComplexType schemaComplexType = new XmlSchemaComplexType();
      XmlSchemaSequence xmlSchemaSequence = new XmlSchemaSequence();
      xmlSchemaSequence.Items.Add((XmlSchemaObject) new XmlSchemaAny()
      {
        Namespace = dsEmvInfo.Namespace
      });
      schemaComplexType.Particle = (XmlSchemaParticle) xmlSchemaSequence;
      XmlSchema schemaSerializable = dsEmvInfo.GetSchemaSerializable();
      if (xs.Contains(schemaSerializable.TargetNamespace))
      {
        MemoryStream memoryStream1 = new MemoryStream();
        MemoryStream memoryStream2 = new MemoryStream();
        try
        {
          schemaSerializable.Write((Stream) memoryStream1);
          foreach (XmlSchema schema in (IEnumerable) xs.Schemas(schemaSerializable.TargetNamespace))
          {
            memoryStream2.SetLength(0L);
            schema.Write((Stream) memoryStream2);
            if (memoryStream1.Length == memoryStream2.Length)
            {
              memoryStream1.Position = 0L;
              memoryStream2.Position = 0L;
              do
                ;
              while (memoryStream1.Position != memoryStream1.Length && memoryStream1.ReadByte() == memoryStream2.ReadByte());
              if (memoryStream1.Position == memoryStream1.Length)
                return schemaComplexType;
            }
          }
        }
        finally
        {
          if (memoryStream1 != null)
            memoryStream1.Close();
          if (memoryStream2 != null)
            memoryStream2.Close();
        }
      }
      xs.Add(schemaSerializable);
      return schemaComplexType;
    }

    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    public delegate void EMV_TAGSRowChangeEventHandler(object sender, DS_EMV_INFO.EMV_TAGSRowChangeEvent e);

    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    public delegate void TAG_BITSRowChangeEventHandler(object sender, DS_EMV_INFO.TAG_BITSRowChangeEvent e);

    [XmlSchemaProvider("GetTypedTableSchema")]
    [Serializable]
    public class EMV_TAGSDataTable : DataTable, IEnumerable
    {
      private DataColumn columnTAG;
      private DataColumn columnName;
      private DataColumn columnDescription;
      private DataColumn columnType;
      private DataColumn columnLength;

      [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
      [DebuggerNonUserCode]
      public EMV_TAGSDataTable()
      {
        this.TableName = "EMV_TAGS";
        this.BeginInit();
        this.InitClass();
        this.EndInit();
      }

      [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
      [DebuggerNonUserCode]
      internal EMV_TAGSDataTable(DataTable table)
      {
        this.TableName = table.TableName;
        if (table.CaseSensitive != table.DataSet.CaseSensitive)
          this.CaseSensitive = table.CaseSensitive;
        if (table.Locale.ToString() != table.DataSet.Locale.ToString())
          this.Locale = table.Locale;
        if (table.Namespace != table.DataSet.Namespace)
          this.Namespace = table.Namespace;
        this.Prefix = table.Prefix;
        this.MinimumCapacity = table.MinimumCapacity;
      }

      [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
      [DebuggerNonUserCode]
      protected EMV_TAGSDataTable(SerializationInfo info, StreamingContext context)
        : base(info, context)
      {
        this.InitVars();
      }

      [DebuggerNonUserCode]
      [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
      public DataColumn TAGColumn
      {
        get
        {
          return this.columnTAG;
        }
      }

      [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
      [DebuggerNonUserCode]
      public DataColumn NameColumn
      {
        get
        {
          return this.columnName;
        }
      }

      [DebuggerNonUserCode]
      [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
      public DataColumn DescriptionColumn
      {
        get
        {
          return this.columnDescription;
        }
      }

      [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
      [DebuggerNonUserCode]
      public DataColumn TypeColumn
      {
        get
        {
          return this.columnType;
        }
      }

      [DebuggerNonUserCode]
      [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
      public DataColumn LengthColumn
      {
        get
        {
          return this.columnLength;
        }
      }

      [Browsable(false)]
      [DebuggerNonUserCode]
      [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
      public int Count
      {
        get
        {
          return this.Rows.Count;
        }
      }

      [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
      [DebuggerNonUserCode]
      public DS_EMV_INFO.EMV_TAGSRow this[int index]
      {
        get
        {
          return (DS_EMV_INFO.EMV_TAGSRow) this.Rows[index];
        }
      }

      [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
      public event DS_EMV_INFO.EMV_TAGSRowChangeEventHandler EMV_TAGSRowChanging;

      [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
      public event DS_EMV_INFO.EMV_TAGSRowChangeEventHandler EMV_TAGSRowChanged;

      [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
      public event DS_EMV_INFO.EMV_TAGSRowChangeEventHandler EMV_TAGSRowDeleting;

      [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
      public event DS_EMV_INFO.EMV_TAGSRowChangeEventHandler EMV_TAGSRowDeleted;

      [DebuggerNonUserCode]
      [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
      public void AddEMV_TAGSRow(DS_EMV_INFO.EMV_TAGSRow row)
      {
        this.Rows.Add((DataRow) row);
      }

      [DebuggerNonUserCode]
      [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
      public DS_EMV_INFO.EMV_TAGSRow AddEMV_TAGSRow(string TAG, string Name, string Description, string Type, string Length)
      {
        DS_EMV_INFO.EMV_TAGSRow emvTagsRow = (DS_EMV_INFO.EMV_TAGSRow) this.NewRow();
        object[] objArray = new object[5]
        {
          (object) TAG,
          (object) Name,
          (object) Description,
          (object) Type,
          (object) Length
        };
        emvTagsRow.ItemArray = objArray;
        this.Rows.Add((DataRow) emvTagsRow);
        return emvTagsRow;
      }

      [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
      [DebuggerNonUserCode]
      public DS_EMV_INFO.EMV_TAGSRow FindByTAG(string TAG)
      {
        return (DS_EMV_INFO.EMV_TAGSRow) this.Rows.Find(new object[1]
        {
          (object) TAG
        });
      }

      [DebuggerNonUserCode]
      [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
      public virtual IEnumerator GetEnumerator()
      {
        return this.Rows.GetEnumerator();
      }

      [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
      [DebuggerNonUserCode]
      public override DataTable Clone()
      {
        DS_EMV_INFO.EMV_TAGSDataTable emvTagsDataTable = (DS_EMV_INFO.EMV_TAGSDataTable) base.Clone();
        emvTagsDataTable.InitVars();
        return (DataTable) emvTagsDataTable;
      }

      [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
      [DebuggerNonUserCode]
      protected override DataTable CreateInstance()
      {
        return (DataTable) new DS_EMV_INFO.EMV_TAGSDataTable();
      }

      [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
      [DebuggerNonUserCode]
      internal void InitVars()
      {
        this.columnTAG = this.Columns["TAG"];
        this.columnName = this.Columns["Name"];
        this.columnDescription = this.Columns["Description"];
        this.columnType = this.Columns["Type"];
        this.columnLength = this.Columns["Length"];
      }

      [DebuggerNonUserCode]
      [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
      private void InitClass()
      {
        this.columnTAG = new DataColumn("TAG", typeof (string), (string) null, MappingType.Element);
        this.Columns.Add(this.columnTAG);
        this.columnName = new DataColumn("Name", typeof (string), (string) null, MappingType.Element);
        this.Columns.Add(this.columnName);
        this.columnDescription = new DataColumn("Description", typeof (string), (string) null, MappingType.Element);
        this.Columns.Add(this.columnDescription);
        this.columnType = new DataColumn("Type", typeof (string), (string) null, MappingType.Element);
        this.Columns.Add(this.columnType);
        this.columnLength = new DataColumn("Length", typeof (string), (string) null, MappingType.Element);
        this.Columns.Add(this.columnLength);
        this.Constraints.Add((Constraint) new UniqueConstraint("Constraint1", new DataColumn[1]
        {
          this.columnTAG
        }, true));
        this.columnTAG.AllowDBNull = false;
        this.columnTAG.Unique = true;
      }

      [DebuggerNonUserCode]
      [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
      public DS_EMV_INFO.EMV_TAGSRow NewEMV_TAGSRow()
      {
        return (DS_EMV_INFO.EMV_TAGSRow) this.NewRow();
      }

      [DebuggerNonUserCode]
      [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
      protected override DataRow NewRowFromBuilder(DataRowBuilder builder)
      {
        return (DataRow) new DS_EMV_INFO.EMV_TAGSRow(builder);
      }

      [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
      [DebuggerNonUserCode]
      protected override Type GetRowType()
      {
        return typeof (DS_EMV_INFO.EMV_TAGSRow);
      }

      [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
      [DebuggerNonUserCode]
      protected override void OnRowChanged(DataRowChangeEventArgs e)
      {
        base.OnRowChanged(e);
        if (this.EMV_TAGSRowChanged == null)
          return;
        this.EMV_TAGSRowChanged((object) this, new DS_EMV_INFO.EMV_TAGSRowChangeEvent((DS_EMV_INFO.EMV_TAGSRow) e.Row, e.Action));
      }

      [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
      [DebuggerNonUserCode]
      protected override void OnRowChanging(DataRowChangeEventArgs e)
      {
        base.OnRowChanging(e);
        if (this.EMV_TAGSRowChanging == null)
          return;
        this.EMV_TAGSRowChanging((object) this, new DS_EMV_INFO.EMV_TAGSRowChangeEvent((DS_EMV_INFO.EMV_TAGSRow) e.Row, e.Action));
      }

      [DebuggerNonUserCode]
      [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
      protected override void OnRowDeleted(DataRowChangeEventArgs e)
      {
        base.OnRowDeleted(e);
        if (this.EMV_TAGSRowDeleted == null)
          return;
        this.EMV_TAGSRowDeleted((object) this, new DS_EMV_INFO.EMV_TAGSRowChangeEvent((DS_EMV_INFO.EMV_TAGSRow) e.Row, e.Action));
      }

      [DebuggerNonUserCode]
      [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
      protected override void OnRowDeleting(DataRowChangeEventArgs e)
      {
        base.OnRowDeleting(e);
        if (this.EMV_TAGSRowDeleting == null)
          return;
        this.EMV_TAGSRowDeleting((object) this, new DS_EMV_INFO.EMV_TAGSRowChangeEvent((DS_EMV_INFO.EMV_TAGSRow) e.Row, e.Action));
      }

      [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
      [DebuggerNonUserCode]
      public void RemoveEMV_TAGSRow(DS_EMV_INFO.EMV_TAGSRow row)
      {
        this.Rows.Remove((DataRow) row);
      }

      [DebuggerNonUserCode]
      [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
      public static XmlSchemaComplexType GetTypedTableSchema(XmlSchemaSet xs)
      {
        XmlSchemaComplexType schemaComplexType = new XmlSchemaComplexType();
        XmlSchemaSequence xmlSchemaSequence = new XmlSchemaSequence();
        DS_EMV_INFO dsEmvInfo = new DS_EMV_INFO();
        XmlSchemaAny xmlSchemaAny1 = new XmlSchemaAny();
        xmlSchemaAny1.Namespace = "http://www.w3.org/2001/XMLSchema";
        xmlSchemaAny1.MinOccurs = new Decimal(0);
        xmlSchemaAny1.MaxOccurs = new Decimal(-1, -1, -1, false, (byte) 0);
        xmlSchemaAny1.ProcessContents = XmlSchemaContentProcessing.Lax;
        xmlSchemaSequence.Items.Add((XmlSchemaObject) xmlSchemaAny1);
        XmlSchemaAny xmlSchemaAny2 = new XmlSchemaAny();
        xmlSchemaAny2.Namespace = "urn:schemas-microsoft-com:xml-diffgram-v1";
        xmlSchemaAny2.MinOccurs = new Decimal(1);
        xmlSchemaAny2.ProcessContents = XmlSchemaContentProcessing.Lax;
        xmlSchemaSequence.Items.Add((XmlSchemaObject) xmlSchemaAny2);
        schemaComplexType.Attributes.Add((XmlSchemaObject) new XmlSchemaAttribute()
        {
          Name = "namespace",
          FixedValue = dsEmvInfo.Namespace
        });
        schemaComplexType.Attributes.Add((XmlSchemaObject) new XmlSchemaAttribute()
        {
          Name = "tableTypeName",
          FixedValue = nameof (EMV_TAGSDataTable)
        });
        schemaComplexType.Particle = (XmlSchemaParticle) xmlSchemaSequence;
        XmlSchema schemaSerializable = dsEmvInfo.GetSchemaSerializable();
        if (xs.Contains(schemaSerializable.TargetNamespace))
        {
          MemoryStream memoryStream1 = new MemoryStream();
          MemoryStream memoryStream2 = new MemoryStream();
          try
          {
            schemaSerializable.Write((Stream) memoryStream1);
            foreach (XmlSchema schema in (IEnumerable) xs.Schemas(schemaSerializable.TargetNamespace))
            {
              memoryStream2.SetLength(0L);
              schema.Write((Stream) memoryStream2);
              if (memoryStream1.Length == memoryStream2.Length)
              {
                memoryStream1.Position = 0L;
                memoryStream2.Position = 0L;
                do
                  ;
                while (memoryStream1.Position != memoryStream1.Length && memoryStream1.ReadByte() == memoryStream2.ReadByte());
                if (memoryStream1.Position == memoryStream1.Length)
                  return schemaComplexType;
              }
            }
          }
          finally
          {
            if (memoryStream1 != null)
              memoryStream1.Close();
            if (memoryStream2 != null)
              memoryStream2.Close();
          }
        }
        xs.Add(schemaSerializable);
        return schemaComplexType;
      }
    }

    [XmlSchemaProvider("GetTypedTableSchema")]
    [Serializable]
    public class TAG_BITSDataTable : DataTable, IEnumerable
    {
      private DataColumn columnTAG;
      private DataColumn columnBIT;
      private DataColumn columnDescription;

      [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
      [DebuggerNonUserCode]
      public TAG_BITSDataTable()
      {
        this.TableName = "TAG_BITS";
        this.BeginInit();
        this.InitClass();
        this.EndInit();
      }

      [DebuggerNonUserCode]
      [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
      internal TAG_BITSDataTable(DataTable table)
      {
        this.TableName = table.TableName;
        if (table.CaseSensitive != table.DataSet.CaseSensitive)
          this.CaseSensitive = table.CaseSensitive;
        if (table.Locale.ToString() != table.DataSet.Locale.ToString())
          this.Locale = table.Locale;
        if (table.Namespace != table.DataSet.Namespace)
          this.Namespace = table.Namespace;
        this.Prefix = table.Prefix;
        this.MinimumCapacity = table.MinimumCapacity;
      }

      [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
      [DebuggerNonUserCode]
      protected TAG_BITSDataTable(SerializationInfo info, StreamingContext context)
        : base(info, context)
      {
        this.InitVars();
      }

      [DebuggerNonUserCode]
      [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
      public DataColumn TAGColumn
      {
        get
        {
          return this.columnTAG;
        }
      }

      [DebuggerNonUserCode]
      [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
      public DataColumn BITColumn
      {
        get
        {
          return this.columnBIT;
        }
      }

      [DebuggerNonUserCode]
      [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
      public DataColumn DescriptionColumn
      {
        get
        {
          return this.columnDescription;
        }
      }

      [Browsable(false)]
      [DebuggerNonUserCode]
      [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
      public int Count
      {
        get
        {
          return this.Rows.Count;
        }
      }

      [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
      [DebuggerNonUserCode]
      public DS_EMV_INFO.TAG_BITSRow this[int index]
      {
        get
        {
          return (DS_EMV_INFO.TAG_BITSRow) this.Rows[index];
        }
      }

      [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
      public event DS_EMV_INFO.TAG_BITSRowChangeEventHandler TAG_BITSRowChanging;

      [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
      public event DS_EMV_INFO.TAG_BITSRowChangeEventHandler TAG_BITSRowChanged;

      [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
      public event DS_EMV_INFO.TAG_BITSRowChangeEventHandler TAG_BITSRowDeleting;

      [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
      public event DS_EMV_INFO.TAG_BITSRowChangeEventHandler TAG_BITSRowDeleted;

      [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
      [DebuggerNonUserCode]
      public void AddTAG_BITSRow(DS_EMV_INFO.TAG_BITSRow row)
      {
        this.Rows.Add((DataRow) row);
      }

      [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
      [DebuggerNonUserCode]
      public DS_EMV_INFO.TAG_BITSRow AddTAG_BITSRow(DS_EMV_INFO.EMV_TAGSRow parentEMV_TAGSRowByEMV_TAGS_TAG_BITS, int BIT, string Description)
      {
        DS_EMV_INFO.TAG_BITSRow tagBitsRow = (DS_EMV_INFO.TAG_BITSRow) this.NewRow();
        object[] objArray = new object[3]
        {
          null,
          (object) BIT,
          (object) Description
        };
        if (parentEMV_TAGSRowByEMV_TAGS_TAG_BITS != null)
          objArray[0] = parentEMV_TAGSRowByEMV_TAGS_TAG_BITS[0];
        tagBitsRow.ItemArray = objArray;
        this.Rows.Add((DataRow) tagBitsRow);
        return tagBitsRow;
      }

      [DebuggerNonUserCode]
      [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
      public DS_EMV_INFO.TAG_BITSRow FindByTAGBIT(string TAG, int BIT)
      {
        return (DS_EMV_INFO.TAG_BITSRow) this.Rows.Find(new object[2]
        {
          (object) TAG,
          (object) BIT
        });
      }

      [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
      [DebuggerNonUserCode]
      public virtual IEnumerator GetEnumerator()
      {
        return this.Rows.GetEnumerator();
      }

      [DebuggerNonUserCode]
      [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
      public override DataTable Clone()
      {
        DS_EMV_INFO.TAG_BITSDataTable tagBitsDataTable = (DS_EMV_INFO.TAG_BITSDataTable) base.Clone();
        tagBitsDataTable.InitVars();
        return (DataTable) tagBitsDataTable;
      }

      [DebuggerNonUserCode]
      [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
      protected override DataTable CreateInstance()
      {
        return (DataTable) new DS_EMV_INFO.TAG_BITSDataTable();
      }

      [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
      [DebuggerNonUserCode]
      internal void InitVars()
      {
        this.columnTAG = this.Columns["TAG"];
        this.columnBIT = this.Columns["BIT"];
        this.columnDescription = this.Columns["Description"];
      }

      [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
      [DebuggerNonUserCode]
      private void InitClass()
      {
        this.columnTAG = new DataColumn("TAG", typeof (string), (string) null, MappingType.Element);
        this.Columns.Add(this.columnTAG);
        this.columnBIT = new DataColumn("BIT", typeof (int), (string) null, MappingType.Element);
        this.Columns.Add(this.columnBIT);
        this.columnDescription = new DataColumn("Description", typeof (string), (string) null, MappingType.Element);
        this.Columns.Add(this.columnDescription);
        this.Constraints.Add((Constraint) new UniqueConstraint("Constraint1", new DataColumn[2]
        {
          this.columnTAG,
          this.columnBIT
        }, true));
        this.columnTAG.AllowDBNull = false;
        this.columnBIT.AllowDBNull = false;
      }

      [DebuggerNonUserCode]
      [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
      public DS_EMV_INFO.TAG_BITSRow NewTAG_BITSRow()
      {
        return (DS_EMV_INFO.TAG_BITSRow) this.NewRow();
      }

      [DebuggerNonUserCode]
      [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
      protected override DataRow NewRowFromBuilder(DataRowBuilder builder)
      {
        return (DataRow) new DS_EMV_INFO.TAG_BITSRow(builder);
      }

      [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
      [DebuggerNonUserCode]
      protected override Type GetRowType()
      {
        return typeof (DS_EMV_INFO.TAG_BITSRow);
      }

      [DebuggerNonUserCode]
      [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
      protected override void OnRowChanged(DataRowChangeEventArgs e)
      {
        base.OnRowChanged(e);
        if (this.TAG_BITSRowChanged == null)
          return;
        this.TAG_BITSRowChanged((object) this, new DS_EMV_INFO.TAG_BITSRowChangeEvent((DS_EMV_INFO.TAG_BITSRow) e.Row, e.Action));
      }

      [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
      [DebuggerNonUserCode]
      protected override void OnRowChanging(DataRowChangeEventArgs e)
      {
        base.OnRowChanging(e);
        if (this.TAG_BITSRowChanging == null)
          return;
        this.TAG_BITSRowChanging((object) this, new DS_EMV_INFO.TAG_BITSRowChangeEvent((DS_EMV_INFO.TAG_BITSRow) e.Row, e.Action));
      }

      [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
      [DebuggerNonUserCode]
      protected override void OnRowDeleted(DataRowChangeEventArgs e)
      {
        base.OnRowDeleted(e);
        if (this.TAG_BITSRowDeleted == null)
          return;
        this.TAG_BITSRowDeleted((object) this, new DS_EMV_INFO.TAG_BITSRowChangeEvent((DS_EMV_INFO.TAG_BITSRow) e.Row, e.Action));
      }

      [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
      [DebuggerNonUserCode]
      protected override void OnRowDeleting(DataRowChangeEventArgs e)
      {
        base.OnRowDeleting(e);
        if (this.TAG_BITSRowDeleting == null)
          return;
        this.TAG_BITSRowDeleting((object) this, new DS_EMV_INFO.TAG_BITSRowChangeEvent((DS_EMV_INFO.TAG_BITSRow) e.Row, e.Action));
      }

      [DebuggerNonUserCode]
      [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
      public void RemoveTAG_BITSRow(DS_EMV_INFO.TAG_BITSRow row)
      {
        this.Rows.Remove((DataRow) row);
      }

      [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
      [DebuggerNonUserCode]
      public static XmlSchemaComplexType GetTypedTableSchema(XmlSchemaSet xs)
      {
        XmlSchemaComplexType schemaComplexType = new XmlSchemaComplexType();
        XmlSchemaSequence xmlSchemaSequence = new XmlSchemaSequence();
        DS_EMV_INFO dsEmvInfo = new DS_EMV_INFO();
        XmlSchemaAny xmlSchemaAny1 = new XmlSchemaAny();
        xmlSchemaAny1.Namespace = "http://www.w3.org/2001/XMLSchema";
        xmlSchemaAny1.MinOccurs = new Decimal(0);
        xmlSchemaAny1.MaxOccurs = new Decimal(-1, -1, -1, false, (byte) 0);
        xmlSchemaAny1.ProcessContents = XmlSchemaContentProcessing.Lax;
        xmlSchemaSequence.Items.Add((XmlSchemaObject) xmlSchemaAny1);
        XmlSchemaAny xmlSchemaAny2 = new XmlSchemaAny();
        xmlSchemaAny2.Namespace = "urn:schemas-microsoft-com:xml-diffgram-v1";
        xmlSchemaAny2.MinOccurs = new Decimal(1);
        xmlSchemaAny2.ProcessContents = XmlSchemaContentProcessing.Lax;
        xmlSchemaSequence.Items.Add((XmlSchemaObject) xmlSchemaAny2);
        schemaComplexType.Attributes.Add((XmlSchemaObject) new XmlSchemaAttribute()
        {
          Name = "namespace",
          FixedValue = dsEmvInfo.Namespace
        });
        schemaComplexType.Attributes.Add((XmlSchemaObject) new XmlSchemaAttribute()
        {
          Name = "tableTypeName",
          FixedValue = nameof (TAG_BITSDataTable)
        });
        schemaComplexType.Particle = (XmlSchemaParticle) xmlSchemaSequence;
        XmlSchema schemaSerializable = dsEmvInfo.GetSchemaSerializable();
        if (xs.Contains(schemaSerializable.TargetNamespace))
        {
          MemoryStream memoryStream1 = new MemoryStream();
          MemoryStream memoryStream2 = new MemoryStream();
          try
          {
            schemaSerializable.Write((Stream) memoryStream1);
            foreach (XmlSchema schema in (IEnumerable) xs.Schemas(schemaSerializable.TargetNamespace))
            {
              memoryStream2.SetLength(0L);
              schema.Write((Stream) memoryStream2);
              if (memoryStream1.Length == memoryStream2.Length)
              {
                memoryStream1.Position = 0L;
                memoryStream2.Position = 0L;
                do
                  ;
                while (memoryStream1.Position != memoryStream1.Length && memoryStream1.ReadByte() == memoryStream2.ReadByte());
                if (memoryStream1.Position == memoryStream1.Length)
                  return schemaComplexType;
              }
            }
          }
          finally
          {
            if (memoryStream1 != null)
              memoryStream1.Close();
            if (memoryStream2 != null)
              memoryStream2.Close();
          }
        }
        xs.Add(schemaSerializable);
        return schemaComplexType;
      }
    }

    public class EMV_TAGSRow : DataRow
    {
      private DS_EMV_INFO.EMV_TAGSDataTable tableEMV_TAGS;

      [DebuggerNonUserCode]
      [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
      internal EMV_TAGSRow(DataRowBuilder rb)
        : base(rb)
      {
        this.tableEMV_TAGS = (DS_EMV_INFO.EMV_TAGSDataTable) this.Table;
      }

      [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
      [DebuggerNonUserCode]
      public string TAG
      {
        get
        {
          return (string) this[this.tableEMV_TAGS.TAGColumn];
        }
        set
        {
          this[this.tableEMV_TAGS.TAGColumn] = (object) value;
        }
      }

      [DebuggerNonUserCode]
      [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
      public string Name
      {
        get
        {
          try
          {
            return (string) this[this.tableEMV_TAGS.NameColumn];
          }
          catch (InvalidCastException ex)
          {
            throw new StrongTypingException("The value for column 'Name' in table 'EMV_TAGS' is DBNull.", (Exception) ex);
          }
        }
        set
        {
          this[this.tableEMV_TAGS.NameColumn] = (object) value;
        }
      }

      [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
      [DebuggerNonUserCode]
      public string Description
      {
        get
        {
          try
          {
            return (string) this[this.tableEMV_TAGS.DescriptionColumn];
          }
          catch (InvalidCastException ex)
          {
            throw new StrongTypingException("The value for column 'Description' in table 'EMV_TAGS' is DBNull.", (Exception) ex);
          }
        }
        set
        {
          this[this.tableEMV_TAGS.DescriptionColumn] = (object) value;
        }
      }

      [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
      [DebuggerNonUserCode]
      public string Type
      {
        get
        {
          try
          {
            return (string) this[this.tableEMV_TAGS.TypeColumn];
          }
          catch (InvalidCastException ex)
          {
            throw new StrongTypingException("The value for column 'Type' in table 'EMV_TAGS' is DBNull.", (Exception) ex);
          }
        }
        set
        {
          this[this.tableEMV_TAGS.TypeColumn] = (object) value;
        }
      }

      [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
      [DebuggerNonUserCode]
      public string Length
      {
        get
        {
          try
          {
            return (string) this[this.tableEMV_TAGS.LengthColumn];
          }
          catch (InvalidCastException ex)
          {
            throw new StrongTypingException("The value for column 'Length' in table 'EMV_TAGS' is DBNull.", (Exception) ex);
          }
        }
        set
        {
          this[this.tableEMV_TAGS.LengthColumn] = (object) value;
        }
      }

      [DebuggerNonUserCode]
      [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
      public bool IsNameNull()
      {
        return this.IsNull(this.tableEMV_TAGS.NameColumn);
      }

      [DebuggerNonUserCode]
      [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
      public void SetNameNull()
      {
        this[this.tableEMV_TAGS.NameColumn] = Convert.DBNull;
      }

      [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
      [DebuggerNonUserCode]
      public bool IsDescriptionNull()
      {
        return this.IsNull(this.tableEMV_TAGS.DescriptionColumn);
      }

      [DebuggerNonUserCode]
      [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
      public void SetDescriptionNull()
      {
        this[this.tableEMV_TAGS.DescriptionColumn] = Convert.DBNull;
      }

      [DebuggerNonUserCode]
      [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
      public bool IsTypeNull()
      {
        return this.IsNull(this.tableEMV_TAGS.TypeColumn);
      }

      [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
      [DebuggerNonUserCode]
      public void SetTypeNull()
      {
        this[this.tableEMV_TAGS.TypeColumn] = Convert.DBNull;
      }

      [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
      [DebuggerNonUserCode]
      public bool IsLengthNull()
      {
        return this.IsNull(this.tableEMV_TAGS.LengthColumn);
      }

      [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
      [DebuggerNonUserCode]
      public void SetLengthNull()
      {
        this[this.tableEMV_TAGS.LengthColumn] = Convert.DBNull;
      }

      [DebuggerNonUserCode]
      [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
      public DS_EMV_INFO.TAG_BITSRow[] GetTAG_BITSRows()
      {
        if (this.Table.ChildRelations["EMV_TAGS_TAG_BITS"] == null)
          return new DS_EMV_INFO.TAG_BITSRow[0];
        return (DS_EMV_INFO.TAG_BITSRow[]) this.GetChildRows(this.Table.ChildRelations["EMV_TAGS_TAG_BITS"]);
      }
    }

    public class TAG_BITSRow : DataRow
    {
      private DS_EMV_INFO.TAG_BITSDataTable tableTAG_BITS;

      [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
      [DebuggerNonUserCode]
      internal TAG_BITSRow(DataRowBuilder rb)
        : base(rb)
      {
        this.tableTAG_BITS = (DS_EMV_INFO.TAG_BITSDataTable) this.Table;
      }

      [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
      [DebuggerNonUserCode]
      public string TAG
      {
        get
        {
          return (string) this[this.tableTAG_BITS.TAGColumn];
        }
        set
        {
          this[this.tableTAG_BITS.TAGColumn] = (object) value;
        }
      }

      [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
      [DebuggerNonUserCode]
      public int BIT
      {
        get
        {
          return (int) this[this.tableTAG_BITS.BITColumn];
        }
        set
        {
          this[this.tableTAG_BITS.BITColumn] = (object) value;
        }
      }

      [DebuggerNonUserCode]
      [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
      public string Description
      {
        get
        {
          try
          {
            return (string) this[this.tableTAG_BITS.DescriptionColumn];
          }
          catch (InvalidCastException ex)
          {
            throw new StrongTypingException("The value for column 'Description' in table 'TAG_BITS' is DBNull.", (Exception) ex);
          }
        }
        set
        {
          this[this.tableTAG_BITS.DescriptionColumn] = (object) value;
        }
      }

      [DebuggerNonUserCode]
      [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
      public DS_EMV_INFO.EMV_TAGSRow EMV_TAGSRow
      {
        get
        {
          return (DS_EMV_INFO.EMV_TAGSRow) this.GetParentRow(this.Table.ParentRelations["EMV_TAGS_TAG_BITS"]);
        }
        set
        {
          this.SetParentRow((DataRow) value, this.Table.ParentRelations["EMV_TAGS_TAG_BITS"]);
        }
      }

      [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
      [DebuggerNonUserCode]
      public bool IsDescriptionNull()
      {
        return this.IsNull(this.tableTAG_BITS.DescriptionColumn);
      }

      [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
      [DebuggerNonUserCode]
      public void SetDescriptionNull()
      {
        this[this.tableTAG_BITS.DescriptionColumn] = Convert.DBNull;
      }
    }

    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    public class EMV_TAGSRowChangeEvent : EventArgs
    {
      private DS_EMV_INFO.EMV_TAGSRow eventRow;
      private DataRowAction eventAction;

      [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
      [DebuggerNonUserCode]
      public EMV_TAGSRowChangeEvent(DS_EMV_INFO.EMV_TAGSRow row, DataRowAction action)
      {
        this.eventRow = row;
        this.eventAction = action;
      }

      [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
      [DebuggerNonUserCode]
      public DS_EMV_INFO.EMV_TAGSRow Row
      {
        get
        {
          return this.eventRow;
        }
      }

      [DebuggerNonUserCode]
      [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
      public DataRowAction Action
      {
        get
        {
          return this.eventAction;
        }
      }
    }

    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    public class TAG_BITSRowChangeEvent : EventArgs
    {
      private DS_EMV_INFO.TAG_BITSRow eventRow;
      private DataRowAction eventAction;

      [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
      [DebuggerNonUserCode]
      public TAG_BITSRowChangeEvent(DS_EMV_INFO.TAG_BITSRow row, DataRowAction action)
      {
        this.eventRow = row;
        this.eventAction = action;
      }

      [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
      [DebuggerNonUserCode]
      public DS_EMV_INFO.TAG_BITSRow Row
      {
        get
        {
          return this.eventRow;
        }
      }

      [DebuggerNonUserCode]
      [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
      public DataRowAction Action
      {
        get
        {
          return this.eventAction;
        }
      }
    }
  }
}
