using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Shapes;
using FreeSCADA.Common.Schema;
using FreeSCADA.Common.Schema.Actions;
using FreeSCADA.Designer.SchemaEditor.PropertiesUtils.PropertyGridTypeEditors;

namespace FreeSCADA.Designer.SchemaEditor.PropertiesUtils
{
    /// <summary>
    /// 
    /// </summary>
	public class PropertyInfo
	{
        /// <summary>
        /// 
        /// </summary>
		public string SourceProperty = "";
        /// <summary>
        /// 
        /// </summary>
		public string TargetProperty = "";
        /// <summary>
        /// 
        /// </summary>
        public Type TargetType=null;
        /// <summary>
        /// 
        /// </summary>
		public string DisplayName = "";
        /// <summary>
        /// 
        /// </summary>
		public string Description = "";
        /// <summary>
        /// 
        /// </summary>
		public string Group = "";
        /// <summary>
        /// 
        /// </summary>
        public Type Editor;
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
		public string GetTargetPropertyName()
		{
			if (string.IsNullOrEmpty(TargetProperty))
				return SourceProperty;
			else
				return TargetProperty;
		}
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
		public string GetTargetPropertyDisplayName()
		{
			if (string.IsNullOrEmpty(DisplayName))
			{
				if (string.IsNullOrEmpty(TargetProperty))
					return SourceProperty;
				else
					return TargetProperty;
			}
			else
				return DisplayName;
		}
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
		public override string ToString()
		{
			return GetTargetPropertyDisplayName();
		}
	}

	static class PropertiesMap
	{
		static Dictionary<Type, List<PropertyInfo>> propertiesMap = new Dictionary<Type, List<PropertyInfo>>();

		static PropertiesMap()
		{
			RegisterPrperties();
		}

        static public void RegisterProperty(System.Type objectType, string sourceProperty, System.Type editor, string description, string group = "")
        {
            PropertiesMap.RegisterProperty(objectType, new PropertyInfo
            {
                SourceProperty = sourceProperty,
                Editor = editor,
                Description = description,
                Group = group
            });
        }

		static public void RegisterProperty(Type objectType, string sourceProperty, string targetProperty, Type editor, string description, string group = "")
		{
			PropertyInfo info = new PropertyInfo();
			info.SourceProperty = sourceProperty;
			info.TargetProperty = targetProperty;
			info.Editor = editor;
			info.Description = description;
			info.Group = group;
			RegisterProperty(objectType, info);
		}

        static public void RegisterProperty(Type objectType, string sourceProperty, string targetProperty, Type editor, string displayName, string description, string group = "")
        {
            PropertyInfo info = new PropertyInfo();
            info.SourceProperty = sourceProperty;
            info.TargetProperty = targetProperty;
            info.Editor = editor;
            info.DisplayName = displayName;
            info.Description = description;
            info.Group = group;
            RegisterProperty(objectType, info);
        }

		static public void RegisterProperty(Type objectType, string sourceProperty, Type editor)
		{
			PropertyInfo info = new PropertyInfo();
			info.SourceProperty = sourceProperty;
			info.Editor = editor;
			RegisterProperty(objectType, info);
		}

		static public void RegisterProperty(Type objectType, string sourceProperty, string targetProperty, Type editor, string description)
		{
			PropertyInfo info = new PropertyInfo();
			info.SourceProperty = sourceProperty;
			info.TargetProperty = targetProperty;
			info.Editor = editor;
			info.Description = description;
			RegisterProperty(objectType, info);
		}
        static public void RegisterProperty(Type objectType, string sourceProperty, string targetProperty, Type editor, string description, Type targetType, string group = "")
        {

            PropertyInfo info = new PropertyInfo();
            info.SourceProperty = sourceProperty;
            info.TargetProperty = targetProperty;
            info.TargetType = targetType;
            info.Editor = editor;
            info.Description = description;
            RegisterProperty(objectType, info);
        }

		static public void RegisterProperty(Type objectType, string sourceProperty)
		{
			PropertyInfo info = new PropertyInfo();
			info.SourceProperty = sourceProperty;
			RegisterProperty(objectType, info);
		}

		static public void RegisterProperty(Type objectType, PropertyInfo propertyInfo)
		{
			if (propertiesMap.ContainsKey(objectType) == false)
				propertiesMap.Add(objectType, new List<PropertyInfo>());
			propertiesMap[objectType].Add(propertyInfo);
		}

		static public List<PropertyInfo> GetProperties(Type type)
		{
			List<PropertyInfo> result = new List<PropertyInfo>();
			do
			{
				if (propertiesMap.ContainsKey(type))
					result.AddRange(propertiesMap[type]);
				type = type.BaseType;
			} while (type != null);

			return result;
		}

		static public void RegisterPrperties()
		{
			//Check duplicating properties
			//RegisterProperty(typeof(FrameworkElement), "Width", "Width1", typeof(DoubleEditor), "Description for width1 property");
			//RegisterProperty(typeof(FrameworkElement), "Width", "Width2", typeof(DoubleEditor), "Description for width2 property");

            RegisterProperty(typeof(FrameworkElement), "Name", "Name", null, "名称(Name)", "名称", "常规");
            RegisterProperty(typeof(FrameworkElement), "Visibility", "Visibility", null, "可见(Visibility)", "可见", "常规");
            RegisterProperty(typeof(FrameworkElement), "RenderTransform", "RenderTransform", null, "变换(RenderTransform)", "变换", "常规");
            RegisterProperty(typeof(FrameworkElement), "RenderTransformOrigin", "RenderTransformOrigin", null, "变换基点(RenderTransformOrigin)", "变换基点", "常规");

            RegisterProperty(typeof(FrameworkElement), "Width", "Width", null, "宽度(Width)", "宽度", "位置尺寸");
            RegisterProperty(typeof(FrameworkElement), "Height", "Height", null, "高度(Height)", "高度", "位置尺寸");

            RegisterProperty(typeof(FrameworkElement), "Background", "Background", typeof(BrushEditor), "背景(Background)", "背景", "颜色");
            RegisterProperty(typeof(FrameworkElement), "Foreground", "Foreground", typeof(BrushEditor), "前景(Background)", "前景", "颜色");
            RegisterProperty(typeof(FrameworkElement), "Opacity", "Opacity", null, "透明度(Opacity)", "透明度", "颜色");

            RegisterProperty(typeof(FrameworkElement), "Canvas.Top", "Canvas.Top", null, "上边距(Top)", "距离顶部距离", "位置尺寸");
            RegisterProperty(typeof(FrameworkElement), "Canvas.Left", "Canvas.Left", null, "左边距(Left)", "距离左边距离", "位置尺寸");
            RegisterProperty(typeof(FrameworkElement), "Canvas.ZIndex", "Canvas.ZIndex", null, "层(ZIndex)", "图形处于的图层，数值越大越前", "位置尺寸");

            RegisterProperty(typeof(RangeBase), "Value", "Value", null, "值(Value)", "数值", "值和范围");
            RegisterProperty(typeof(RangeBase), "Maximum", "Maximum", null, "最大值(Maximum)", "最大值", "值和范围");
            RegisterProperty(typeof(RangeBase), "Minimum", "Minimum", null, "最小值(Minimum)", "最小值", "值和范围");
            RegisterProperty(typeof(RangeBase), "Orientation", "Orientation", null, "方向(Orientation)", "基点", "值和范围");
            RegisterProperty(typeof(System.Windows.Controls.Primitives.ButtonBase), "Content", "Image", typeof(ImageEditor), "图形(Image)", "内容", "文字和图形");
            RegisterProperty(typeof(System.Windows.Controls.Primitives.ButtonBase), "Content", "Text", null, "内容(Content)", "内容", "文字和图形");
            RegisterProperty(typeof(FreeSCADA.Common.Schema.AnimatedImage), "ImageName", "Image", typeof(ImageEditor), "图形(Image)", "图形", "文字和图形");
            RegisterProperty(typeof(FreeSCADA.Common.Schema.AnimatedImage), "AnimatedControl", "AnimatedControl", null, "图形控制(AnimatedControl)", "图形控制", "文字和图形");
            RegisterProperty(typeof(Control), "Style", "Style", typeof(StyleEditor), "样式(Style)", "样式", "文字和图形");
            RegisterProperty(typeof(Shape), "StrokeThickness", "StrokeThickness", null, "线条宽度(StrokeThickness)", "线条宽度", "颜色");
            RegisterProperty(typeof(Shape), "Stroke", "Stroke", typeof(BrushEditor), "线条颜色(Stroke)", "线条", "颜色");
            RegisterProperty(typeof(Shape), "Fill", "Fill", typeof(BrushEditor), "填充(Fill)", "填充", "颜色");
			RegisterProperty(typeof(BaseAction), "ActionChannelName", typeof(ChannelSelectEditor));
			RegisterProperty(typeof(BaseAction), "MinChannelValue", null);
			RegisterProperty(typeof(BaseAction), "MaxChannelValue", null);
			RegisterProperty(typeof(RotateAction), "MinAngle", null);
			RegisterProperty(typeof(RotateAction), "MaxAngle", null);
            RegisterProperty(typeof(TextBlock), "Text", "Text", null, "文字(Text)", "内容", "字体");
            RegisterProperty(typeof(TextBlock), "FontFamily", "FontFamily", null, "字体(FontFamily)", "字体名称", "字体");
            RegisterProperty(typeof(TextBlock), "FontSize", "FontSize", null, "字号(FontSize)", "字体尺寸", "字体");
            RegisterProperty(typeof(TextBlock), "FontStretch", "FontStretch", null, "拉伸(FontStretch)", "字体拉伸", "字体");
            RegisterProperty(typeof(TextBlock), "FontStyle", "FontStyle", null, "字体(FontStyle)", "字体样式", "字体");
            RegisterProperty(typeof(TextBlock), "FontWeight", "FontWeight", null, "深度(FontWeight)", "字体颜色深度", "字体");
            RegisterProperty(typeof(TextBlock), "TextAlignment", "TextAlignment", null, "对齐方式(TextAlignment)", "文字对齐方向", "文字和图形");

            RegisterProperty(typeof(CheckBox), "IsChecked", "IsChecked", null, "选中(IsChecked)", "是否选中", "控件");
            RegisterProperty(typeof(CheckBox), "Content", "Text", null, "内容(Text)", "内容", "文字和图形");

            RegisterProperty(typeof(TextBox), "Text", "Text", null, "文字(Text)", "内容", "字体");
            RegisterProperty(typeof(TextBox), "TextAlignment", "TextAlignment", null, "对齐方式(TextAlignment)", "文字对齐方向", "文字和图形");

            RegisterProperty(typeof(TimeChartControl), "ChartPeriod", "TimeInterval", null, "时间间隔(TimeInterval)", "时间间隔", "图表");
            RegisterProperty(typeof(TimeChartControl), "ChartScale", "ChartScale", null, "图表比例(ChartScale)", "图表比例", "图表");
            RegisterProperty(typeof(TimeChartControl), "Trends", "Trends", null, "趋势(Trends)", "趋势", "图表");
            RegisterProperty(typeof(TimeChartControl), "ChartName", "ChartName", null, "图表名称(ChartName)", "图表名称", "图表");
            RegisterProperty(typeof(Canvas), "GridManager.GridOn", "GridOn", null, "网格控制(GridOn)", "打开网格", "网格");
            RegisterProperty(typeof(Canvas), "GridManager.GridDelta", "GridDelta", null, "网格距离(GridDelta)", "网格间距", "网格");
            RegisterProperty(typeof(Canvas), "GridManager.ShowGrid", "ShowGrid", null, "显示网格(ShowGrid)", "显示网格", "网格");
            //RegisterProperty(typeof(Canvas), "Margin", null);             //Marging is working, but I think margin have no use here
            //RegisterProperty(typeof(Canvas), "OriginX", null);            //It seems originx is not working
            //RegisterProperty(typeof(Canvas), "OriginY", null);
            RegisterProperty(typeof(ToggleButton), "IsChecked", "IsChecked", null, "选中(IsChecked)", "是否选中", "控件");
		}

	}
}
