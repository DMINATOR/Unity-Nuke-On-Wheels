
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

//Generated on: 15.01.2020 20:53.45
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
		VEHICLE_CAMERA_MIN_LIMIT_Y,
		VEHICLE_CAMERA_MAX_LIMIT_Y,
		VEHICLE_CAMERA_MIN_DISTANCE,
		VEHICLE_CAMERA_MAX_DISTANCE,
		BLOCK_SIZE,
		BLOCK_OUT_RESCALE,

    }

    public static void Load()
    {
		SettingsController.Instance.AddSetting(new SettingValue()
		{
			Name = Enum.GetName(typeof(SettingsConstants.Name), Name.MUSIC_VOLUME),
			Type = SettingValueType.Float,
			MinValue = "0.0",
			DefaultValue = "0.5",
			MaxValue = "1.0"
		});
		SettingsController.Instance.AddSetting(new SettingValue()
		{
			Name = Enum.GetName(typeof(SettingsConstants.Name), Name.SOUND_VOLUME),
			Type = SettingValueType.Float,
			MinValue = "0.0",
			DefaultValue = "0.7",
			MaxValue = "1.0"
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
			DefaultValue = "0.2",
			MaxValue = "1"
		});
		SettingsController.Instance.AddSetting(new SettingValue()
		{
			Name = Enum.GetName(typeof(SettingsConstants.Name), Name.VEHICLE_CAMERA_SPEED_X),
			Type = SettingValueType.Float,
			MinValue = "1",
			DefaultValue = "0.5",
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
		SettingsController.Instance.AddSetting(new SettingValue()
		{
			Name = Enum.GetName(typeof(SettingsConstants.Name), Name.VEHICLE_CAMERA_MIN_LIMIT_Y),
			Type = SettingValueType.Float,
			MinValue = "-180",
			DefaultValue = "10",
			MaxValue = "1000"
		});
		SettingsController.Instance.AddSetting(new SettingValue()
		{
			Name = Enum.GetName(typeof(SettingsConstants.Name), Name.VEHICLE_CAMERA_MAX_LIMIT_Y),
			Type = SettingValueType.Float,
			MinValue = "-180",
			DefaultValue = "80",
			MaxValue = "1000"
		});
		SettingsController.Instance.AddSetting(new SettingValue()
		{
			Name = Enum.GetName(typeof(SettingsConstants.Name), Name.VEHICLE_CAMERA_MIN_DISTANCE),
			Type = SettingValueType.Float,
			MinValue = "0",
			DefaultValue = "0.5",
			MaxValue = "1000"
		});
		SettingsController.Instance.AddSetting(new SettingValue()
		{
			Name = Enum.GetName(typeof(SettingsConstants.Name), Name.VEHICLE_CAMERA_MAX_DISTANCE),
			Type = SettingValueType.Float,
			MinValue = "0",
			DefaultValue = "15",
			MaxValue = "1000"
		});
		SettingsController.Instance.AddSetting(new SettingValue()
		{
			Name = Enum.GetName(typeof(SettingsConstants.Name), Name.BLOCK_SIZE),
			Type = SettingValueType.Integer,
			MinValue = "0",
			DefaultValue = "15",
			MaxValue = "1000"
		});
		SettingsController.Instance.AddSetting(new SettingValue()
		{
			Name = Enum.GetName(typeof(SettingsConstants.Name), Name.BLOCK_OUT_RESCALE),
			Type = SettingValueType.Float,
			MinValue = "0",
			DefaultValue = "1",
			MaxValue = "1000"
		});

    }
}
