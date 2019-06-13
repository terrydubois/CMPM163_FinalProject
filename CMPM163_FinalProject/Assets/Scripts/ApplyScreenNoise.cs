// Original source: https://github.com/staffantan/unity-vhsglitch

using UnityEngine;
using UnityEngine.Video;

[ExecuteInEditMode]
[AddComponentMenu("Image Effects/GlitchEffect")]

// require this GameObject to have a Camera and VideoPlayer (for noise video source)
[RequireComponent(typeof(Camera))]
[RequireComponent(typeof(VideoPlayer))]
public class ApplyScreenNoise : MonoBehaviour
{
	// shader source and glitch noise source
	public Shader shader;
	public VideoClip noiseSourceVideo;

	// intensity of horizontal and vertical distortion
	public float xScanlineAmp = 0;
	public float yScanlineAmp = 0;

	// placement of distortion
	private float _yScanline;
	private float _xScanline;

	// create empty material and get VideoPlayer for this GameObject
	private Material _material = null;
	private VideoPlayer _player;

	void Start()
	{
		// set material to be playing glitch source video
		_material = new Material(shader);
		_player = GetComponent<VideoPlayer>();
		_player.isLooping = true;
		_player.renderMode = VideoRenderMode.APIOnly;
		_player.audioOutputMode = VideoAudioOutputMode.None;
		_player.clip = noiseSourceVideo;
		_player.Play();
	}

	void OnRenderImage(RenderTexture source, RenderTexture destination)
	{
		// set noise source texture
		_material.SetTexture("_NoiseSourceTex", _player.texture);

		// apply intensity to distortion and move distortion based on time
		_yScanline += Time.deltaTime * yScanlineAmp;
		_xScanline -= Time.deltaTime * xScanlineAmp;

		// give new random position is scanlines go out of bounds
		if (_yScanline >= 1) {
			_yScanline = Random.value;
		}
		if (_xScanline <= 0 || Random.value < 0.05) {
			_xScanline = Random.value;
		}

		// apply scanline positions to material shader
		_material.SetFloat("_yScanline", _yScanline);
		_material.SetFloat("_xScanline", _xScanline);

		// copies texture to destination, now with shader!
		Graphics.Blit(source, destination, _material);
	}

	protected void OnDisable()
	{
		if (_material) {
			DestroyImmediate(_material);
		}
	}
}
