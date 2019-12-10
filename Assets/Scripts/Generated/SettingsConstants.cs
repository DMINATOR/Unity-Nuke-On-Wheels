
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

//Generated on: 10.12.2019 22:22.34
public class SettingsConstants
{

    public enum Name
    {
		MUSIC_VOLUME,
		SOUND_VOLUME,
		TIME_CONTROL_ELEMENTS_SIZE,
		TIME_CONTROL_CHANGE_DIFFERENCE,
		VEHICLE_CAMERA_SPEED_X,
		VEHICLE_CAMERA_SPEED_Y,

    }

    public static void Load()
    {
		SettingsController.Instance.AddSetting(new SettingValue()
		{
			Name = Enum.GetName(typeof(SettingsConstants.Name), Name.MUSIC_VOLUME),
			Type = SettingValueType.Float,
			MinValue = "0.0f",
			DefaultValue = "0.5f",
			MaxValue = "1.0f"
		});
		SettingsController.Instance.AddSetting(new SettingValue()
		{
			Name = Enum.GetName(typeof(SettingsConstants.Name), Name.SOUND_VOLUME),
			Type = SettingValueType.Float,
			MinValue = "0.0f",
			DefaultValue = "0.7f",
			MaxValue = "1.0f"
		});
		SettingsController.Instance.AddSetting(new SettingValue()
		{
			Name = Enum.GetName(typeof(SettingsConstants.Name), Name.TIME_CONTROL_ELEMENTS_SIZE),
			Type = SettingValueType.Integer,
			MinValue = "0",
			DefaultValue = "100",
			MaxValue = "10000"
		});
		SettingsController.Instance.AddSetting(new SettingValue()
		{
			Name = Enum.GetName(typeof(SettingsConstants.Name), Name.TIME_CONTROL_CHANGE_DIFFERENCE),
			Type = SettingValueType.Float,
			MinValue = "0",
			DefaultValue = "0,2",
			MaxValue = "1"
		});
		SettingsController.Instance.AddSetting(new SettingValue()
		{
			Name = Enum.GetName(typeof(SettingsConstants.Name), Name.VEHICLE_CAMERA_SPEED_X),
			Type = SettingValueType.Float,
			MinValue = "1",
			DefaultValue = "1",
			MaxValue = "1000"
		});
		SettingsController.Instance.AddSetting(new SettingValue()
		{
			Name = Enum.GetName(typeof(SettingsConstants.Name), Name.VEHICLE_CAMERA_SPEED_Y),
			Type = SettingValueType.Float,
			MinValue = "1",
			DefaultValue = "1",
			MaxValue = "1000"
		});

    }
}
