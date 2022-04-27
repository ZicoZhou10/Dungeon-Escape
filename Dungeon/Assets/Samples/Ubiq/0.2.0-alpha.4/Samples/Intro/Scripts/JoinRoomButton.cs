using System.Collections;
using System.Collections.Generic;
using Ubiq.Rooms;
using UnityEngine;
using UnityEngine.UI;

namespace Ubiq.Samples {
    public class JoinRoomButton : MonoBehaviour {
        public SocialMenu mainMenu;
        public PanelSwitcher mainPanel;
        public Text joincodeText;
        public TextEntry textEntry;
        public Image textInputArea;
        public string failMessage;
        public Color failTextInputAreaColor;

        private Color defaultTextInputAreaColor;
        private string lastRequestedJoincode;

        private void OnEnable () {
            mainMenu.roomClient.OnJoinedRoom.AddListener (RoomClient_OnJoinedRoom);
            mainMenu.roomClient.OnJoinRejected.AddListener (RoomClient_OnJoinRejected);
            defaultTextInputAreaColor = textInputArea.color;
        }

        private void OnDisable () {
            mainMenu.roomClient.OnJoinedRoom.RemoveListener (RoomClient_OnJoinedRoom);
            mainMenu.roomClient.OnJoinRejected.RemoveListener (RoomClient_OnJoinRejected);
            textInputArea.color = defaultTextInputAreaColor;
        }

        private void RoomClient_OnJoinedRoom (IRoom room) {
            mainPanel.SwitchPanelToDefault ();
            GameObject player = GameObject.Find ("Player");
            // player.transform.position = new Vector3(1.208f, 88.5f, 35.82f);
            player.transform.position = new Vector3 (1.253f, 103.0f, 10.5f);
            player.GetComponent<StartDoorOpen> ().playerType = 2;
            //Debug.Log(StartDoorOpen.playerType);
        }

        private void RoomClient_OnJoinRejected (Rejection rejection) {
            if (rejection.joincode != lastRequestedJoincode) {
                return;
            }

            textEntry.SetText (failMessage, textEntry.defaultTextColor, true);
            textInputArea.color = failTextInputAreaColor;
        }

        // Expected to be called by a UI element
        public void Join () {
            lastRequestedJoincode = joincodeText.text.ToLowerInvariant ();
            mainMenu.roomClient.Join (joincode: lastRequestedJoincode);
        }
    }
}