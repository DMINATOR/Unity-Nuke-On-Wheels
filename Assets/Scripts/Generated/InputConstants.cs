
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

//Generated on: 11.12.2019 21:21.21
public class InputMapping
{

    public enum InputMappingKeyName
    {
		Horizontal,
		Vertical,
		Fire1,
		Camera_Toggle,
		Fire2,
		Fire3,
		Jump,
		MouseY,
		Camera_Rotate_Y,
		MouseX,
		Camera_Rotate_X,
		MouseScrollWheel,
		Camera_Zoom,
		Submit,
		Cancel,

    }

    public static void Load()
    {
		InputController.Instance.Add(InputMappingKeyName.Horizontal, new List<InputMappingKey>()
		{
			new InputMappingKey(){
			 Name = "Horizontal", DescriptiveName = "", DescriptiveNegativeName = "", NegativeButton = "left", PositiveButton = "right", AltNegativeButton = "a", AltPositiveButton = "d"
			}
			,
			new InputMappingKey(){
			 Name = "Horizontal", DescriptiveName = "", DescriptiveNegativeName = "", NegativeButton = "", PositiveButton = "", AltNegativeButton = "", AltPositiveButton = ""
			}
		});
		InputController.Instance.Add(InputMappingKeyName.Vertical, new List<InputMappingKey>()
		{
			new InputMappingKey(){
			 Name = "Vertical", DescriptiveName = "", DescriptiveNegativeName = "", NegativeButton = "down", PositiveButton = "up", AltNegativeButton = "s", AltPositiveButton = "w"
			}
			,
			new InputMappingKey(){
			 Name = "Vertical", DescriptiveName = "", DescriptiveNegativeName = "", NegativeButton = "", PositiveButton = "", AltNegativeButton = "", AltPositiveButton = ""
			}
		});
		InputController.Instance.Add(InputMappingKeyName.Fire1, new List<InputMappingKey>()
		{
			new InputMappingKey(){
			 Name = "Fire1", DescriptiveName = "", DescriptiveNegativeName = "", NegativeButton = "", PositiveButton = "left ctrl", AltNegativeButton = "", AltPositiveButton = "mouse 0"
			}
			,
			new InputMappingKey(){
			 Name = "Fire1", DescriptiveName = "", DescriptiveNegativeName = "", NegativeButton = "", PositiveButton = "joystick button 0", AltNegativeButton = "", AltPositiveButton = ""
			}
		});
		InputController.Instance.Add(InputMappingKeyName.Camera_Toggle, new List<InputMappingKey>()
		{
			new InputMappingKey(){
			 Name = "Camera_Toggle", DescriptiveName = "", DescriptiveNegativeName = "", NegativeButton = "", PositiveButton = "c", AltNegativeButton = "", AltPositiveButton = "mouse 0"
			}
		});
		InputController.Instance.Add(InputMappingKeyName.Fire2, new List<InputMappingKey>()
		{
			new InputMappingKey(){
			 Name = "Fire2", DescriptiveName = "", DescriptiveNegativeName = "", NegativeButton = "", PositiveButton = "left alt", AltNegativeButton = "", AltPositiveButton = "mouse 1"
			}
			,
			new InputMappingKey(){
			 Name = "Fire2", DescriptiveName = "", DescriptiveNegativeName = "", NegativeButton = "", PositiveButton = "joystick button 1", AltNegativeButton = "", AltPositiveButton = ""
			}
		});
		InputController.Instance.Add(InputMappingKeyName.Fire3, new List<InputMappingKey>()
		{
			new InputMappingKey(){
			 Name = "Fire3", DescriptiveName = "", DescriptiveNegativeName = "", NegativeButton = "", PositiveButton = "left shift", AltNegativeButton = "", AltPositiveButton = "mouse 2"
			}
			,
			new InputMappingKey(){
			 Name = "Fire3", DescriptiveName = "", DescriptiveNegativeName = "", NegativeButton = "", PositiveButton = "joystick button 2", AltNegativeButton = "", AltPositiveButton = ""
			}
		});
		InputController.Instance.Add(InputMappingKeyName.Jump, new List<InputMappingKey>()
		{
			new InputMappingKey(){
			 Name = "Jump", DescriptiveName = "", DescriptiveNegativeName = "", NegativeButton = "", PositiveButton = "space", AltNegativeButton = "", AltPositiveButton = ""
			}
			,
			new InputMappingKey(){
			 Name = "Jump", DescriptiveName = "", DescriptiveNegativeName = "", NegativeButton = "", PositiveButton = "joystick button 3", AltNegativeButton = "", AltPositiveButton = ""
			}
		});
		InputController.Instance.Add(InputMappingKeyName.MouseY, new List<InputMappingKey>()
		{
			new InputMappingKey(){
			 Name = "Mouse Y", DescriptiveName = "", DescriptiveNegativeName = "", NegativeButton = "", PositiveButton = "", AltNegativeButton = "", AltPositiveButton = ""
			}
		});
		InputController.Instance.Add(InputMappingKeyName.Camera_Rotate_Y, new List<InputMappingKey>()
		{
			new InputMappingKey(){
			 Name = "Camera_Rotate_Y", DescriptiveName = "", DescriptiveNegativeName = "", NegativeButton = "", PositiveButton = "", AltNegativeButton = "", AltPositiveButton = ""
			}
		});
		InputController.Instance.Add(InputMappingKeyName.MouseX, new List<InputMappingKey>()
		{
			new InputMappingKey(){
			 Name = "Mouse X", DescriptiveName = "", DescriptiveNegativeName = "", NegativeButton = "", PositiveButton = "", AltNegativeButton = "", AltPositiveButton = ""
			}
		});
		InputController.Instance.Add(InputMappingKeyName.Camera_Rotate_X, new List<InputMappingKey>()
		{
			new InputMappingKey(){
			 Name = "Camera_Rotate_X", DescriptiveName = "", DescriptiveNegativeName = "", NegativeButton = "", PositiveButton = "", AltNegativeButton = "", AltPositiveButton = ""
			}
		});
		InputController.Instance.Add(InputMappingKeyName.MouseScrollWheel, new List<InputMappingKey>()
		{
			new InputMappingKey(){
			 Name = "Mouse ScrollWheel", DescriptiveName = "", DescriptiveNegativeName = "", NegativeButton = "", PositiveButton = "", AltNegativeButton = "", AltPositiveButton = ""
			}
		});
		InputController.Instance.Add(InputMappingKeyName.Camera_Zoom, new List<InputMappingKey>()
		{
			new InputMappingKey(){
			 Name = "Camera_Zoom", DescriptiveName = "", DescriptiveNegativeName = "", NegativeButton = "", PositiveButton = "", AltNegativeButton = "", AltPositiveButton = ""
			}
		});
		InputController.Instance.Add(InputMappingKeyName.Submit, new List<InputMappingKey>()
		{
			new InputMappingKey(){
			 Name = "Submit", DescriptiveName = "", DescriptiveNegativeName = "", NegativeButton = "", PositiveButton = "return", AltNegativeButton = "", AltPositiveButton = "joystick button 0"
			}
			,
			new InputMappingKey(){
			 Name = "Submit", DescriptiveName = "", DescriptiveNegativeName = "", NegativeButton = "", PositiveButton = "return", AltNegativeButton = "", AltPositiveButton = "joystick button 0"
			}
			,
			new InputMappingKey(){
			 Name = "Submit", DescriptiveName = "", DescriptiveNegativeName = "", NegativeButton = "", PositiveButton = "enter", AltNegativeButton = "", AltPositiveButton = "space"
			}
		});
		InputController.Instance.Add(InputMappingKeyName.Cancel, new List<InputMappingKey>()
		{
			new InputMappingKey(){
			 Name = "Cancel", DescriptiveName = "", DescriptiveNegativeName = "", NegativeButton = "", PositiveButton = "", AltNegativeButton = "", AltPositiveButton = "joystick button 1"
			}
		});

    }
}
