using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.SceneManagement;
using RenderHeads.Media.AVProVideo;
using System.IO;
using System.Text;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
	public static UIController instance=null;
	//获取allController
	private AllController allController=null;

	//scan对象
	public GameObject scanObj;
	//控制light对象
	public GameObject toggleLight;
	//控制对焦的btn对象
	public GameObject focuObj;

	public GameObject dragObj;

	public GameObject vedioImage;

	public MediaPlayer media;

	public GameObject textPanel;

	public GameObject vedioPanel;

	public GameObject DropDownObj;

	public GameObject voiceBtn;

	public GameObject textInfo;

	public AudioClip[] clips;

	private AudioSource audioCtl;

	//vedio是否在播放
	private bool isPlay=false;

	private Tweener vedioMoveTween=null;

	//声明一个回掉的委托
	public delegate void CloseTextPanelDelegate();

	public CloseTextPanelDelegate closeDel=null;

	//要加载的文本名称
	private string textName="";

	void Awake()
	{
		instance = this;
	}

	void Start()
	{
		allController = GameObject.FindGameObjectWithTag ("AllController").GetComponent<AllController>();

		audioCtl = GetComponent<AudioSource> ();

	}

	/// <summary>
	/// Sets the scan.设置扫描框显式与否
	/// </summary>
	/// <param name="visiable">If set to <c>true</c> visiable.</param>
	public void SetScan(bool visiable)
	{
		//当出入的状态和自身不一致的时候执行
		if (scanObj.activeSelf != visiable) {
			scanObj.SetActive (visiable);
		}
	}

	void Update()
	{
		if (media.Control.IsFinished ()) {
			if (!vedioImage.activeSelf) {
				vedioImage.SetActive (true);
			}
		}

		//当名称发生改变的时候，将名字赋值给当前的textName
		if (!string.IsNullOrEmpty (allController.trackName)) {
			if (!string.Equals (allController.trackName, textName)) {
				textName = allController.trackName;
				LoadLocalText (textName);
			}
		}
	}

	/// <summary>
	/// Lights the control.控制闪光灯的开关方法
	/// </summary>
	/// <param name="toggle">If set to <c>true</c> toggle.</param>
	public void LightControl(bool toggle)
	{
		toggleLight.GetComponent<ToggleBtn> ().LightControl (toggle);
	}

	/// <summary>
	/// Cameras the focuing.控制摄像机自动对焦
	/// </summary>
	public void CameraFocuing()
	{
		focuObj.GetComponent<FocuingButton> ().OnFocuing ();
	}

	/// <summary>
	/// Backs the come scene.切换到进入前的场景
	/// </summary>
	public void BackComeScene()
	{
		SceneManager.LoadScene (0);
	}

	/// <summary>
	/// Rests the button.按下reset按钮，重置对象的位置信息
	/// </summary>
	public void  RestBtn()
	{
		if (!string.IsNullOrEmpty (allController.trackName)) {
			allController.AutoCombineBtn (allController.trackName);
			GameObject.FindGameObjectWithTag (allController.trackName).GetComponent<ModelBehaviour>().ResetStartState();
		}
	}

	/// <summary>
	/// Combines the button.响应合并的按钮
	/// </summary>
	public void CombineBtn()
	{
		if (!string.IsNullOrEmpty (allController.trackName)) {
			allController.AutoCombineBtn (allController.trackName);
			//可以加一些合并的特效
		}
	}

	/// <summary>
	/// Splits the button.响应拆分的按钮
	/// </summary>
	public void SplitBtn()
	{
		if (!string.IsNullOrEmpty (allController.trackName)) {
			allController.AutoSplitBtn (allController.trackName);
			//可以加一些拆分的效果
		}
	}

	/// <summary>
	/// Drags the toggle.注册Dragtoggle按钮
	/// </summary>
	public void DragToggle(bool toggle)
	{
		dragObj.GetComponent<ToggleBtn> ().DragToggle (toggle);
	}

	/// <summary>
	/// Vedios the toggle.
	/// </summary>
	public void VedioBtn()
	{
		isPlay = !isPlay;

		if (isPlay) {
			vedioImage.SetActive (false);
			media.Control.Play ();
		} else {
			vedioImage.SetActive (true);
			media.Control.Pause ();
		}
		if (media.Control.IsFinished ()) {
			media.Control.Rewind ();
			vedioImage.SetActive (false);
			media.Control.Play ();
		}
			
	}

	/// <summary>
	/// Res the start vedio.重新播放动画
	/// </summary>
	public void ReStartVedio()
	{
		if (vedioImage.activeSelf) {
			vedioImage.SetActive (false);
		}
		media.Control.Rewind();
	}


	/// <summary>
	/// Raises the click tag move text event.移动TextPanel面板
	/// </summary>
	public void MoveTextPanel(CloseTextPanelDelegate _closeDel)
	{
		closeDel = _closeDel;
		//最好用rectTransform
		textPanel.GetComponent<RectTransform>().DOLocalRotate (new Vector3(0,30f,0),1.5f);
	}

	/// <summary>
	/// Closes the text panel.关闭TextPanel
	/// </summary>
	public void CloseTextPanel()
	{
		if (closeDel != null) {
			closeDel ();
		}
		textPanel.GetComponent<RectTransform>().DOLocalRotate (new Vector3(0,240f,0),3f,RotateMode.FastBeyond360);
		DropDownObj.SetActive (true);

		//将声音关闭、
		audioCtl.Stop();

	}

	/// <summary>
	/// Moves the vedio panel.将视频的面板移动出来
	/// </summary>
	public void MoveVedioPanel()
	{
		DropDownObj.SetActive (false);
		if (null!=vedioMoveTween) {
			vedioMoveTween.PlayForward ();
		} else {
			vedioMoveTween= vedioPanel.GetComponent<RectTransform> ().DOLocalMoveY (0, 1.5f);
			vedioMoveTween.SetAutoKill (false);
		}
	}

	/// <summary>
	/// Closes the vedio panel.关闭视频面板
	/// </summary>
	public void CloseVedioPanel()
	{
		media.Control.Stop ();

		vedioMoveTween.PlayBackwards ();

		DropDownObj.SetActive (true);
	}

	/// <summary>
	/// Voices the button change.控制声音的播放
	/// </summary>
	public void VoiceBtnChange()
	{
		AudioClip nowClip=null;
		if (!string.IsNullOrEmpty (textName)) {
			for (int i = 0; i < clips.Length; i++) {
				if (clips [i].name == textName) {
					nowClip = clips [i];
					break;
				}
			}


			audioCtl.clip = nowClip;
			audioCtl.Play ();
		}



	}

	/// <summary>
	/// Loads the local text.用于读取本地的文字
	/// </summary>
	/// <param name="index">Index.</param>
	public void LoadLocalText(string name)
	{
		string path =Application.dataPath+"/MyText/"+name+".txt";
		StreamReader sr = new StreamReader (path,Encoding.UTF8);
		textInfo.GetComponent<Text>().text =sr.ReadToEnd();

		Debug.Log (path);
	}


	public void TextPanelDisplay()
	{
		MoveTextPanel (
			()=>{}
		);
	}
}
