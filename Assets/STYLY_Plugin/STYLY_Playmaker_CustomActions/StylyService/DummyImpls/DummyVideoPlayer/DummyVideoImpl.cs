using System;
using System.Collections.Generic;
using UnityEngine;

namespace STYLY
{
    /// <summary>
    /// DummyVideoPlayerを利用したStylyServiceVideoの実装。
    /// Unity Editorでアップロード前確認時に動くものとして利用する。
    /// このクラスは橋渡し程度。
    /// </summary>
    public class DummyVideoImpl : IStylyServiceVideoImpl
    {
        public void VideoInit(GameObject targetObj, VideoParams videoParams, Action<Exception> onFinished)
        {
            var player = targetObj.GetComponent<DummyVideoPlayer>();
            if (player == null)
            {
                player = targetObj.AddComponent<DummyVideoPlayer>();
            }
            player.Init(videoParams, onFinished);
        }

        public void VideoPlay(GameObject targetObj, Action<Exception> onFinished)
        {
            var player = GetDummyVideoPlayerOrError(targetObj, "Video Play", onFinished);
            if (player != null)
            {
                player.Play(onFinished);
            }
        }

        public void VideoStop(GameObject targetObj, Action<Exception> onFinished)
        {
            var player = GetDummyVideoPlayerOrError(targetObj, "Video Stop", onFinished);
            if (player != null)
            {
                player.Stop(onFinished);
            }
        }

        public void VideoPause(GameObject targetObj, Action<Exception> onFinished)
        {
            var player = GetDummyVideoPlayerOrError(targetObj, "Video Pause", onFinished);
            if (player != null)
            {
                player.Pause(onFinished);
            }
        }

        public void VideoSetVolume(GameObject targetObj, float volume, Action<Exception> onFinished)
        {
            var player = GetDummyVideoPlayerOrError(targetObj, "Video Play", onFinished);
            if (player != null)
            {
                player.SetVolume(volume, onFinished);
            }
        }

        /// <summary>
        /// DummyVideoPlayerを取得し、なければエラー処理をする。
        /// </summary>
        /// <param name="targetObj"></param>
        /// <param name="actionName"></param>
        /// <param name="onFinished"></param>
        /// <returns></returns>
        private DummyVideoPlayer GetDummyVideoPlayerOrError(GameObject targetObj, string actionName, Action<Exception> onFinished)
        {
            var player = targetObj.GetComponent<DummyVideoPlayer>();
            if (player == null)
            {
                var msg = $"{actionName}: No video player found. GameObject:{targetObj.name}";
                Debug.LogError(msg);
                onFinished?.Invoke(new Exception(msg));
                return null;
            }
            return player;
        }
    }
}
