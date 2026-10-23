using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.XR;
using Unity.XR.CoreUtils;
using UnityEngine.XR;
namespace SebraeLabVR.Editor
{
    public static class CreateDemoScene
    {
        [MenuItem("SebraeLab/ Criar cena demonstrativa")]
        public static void Create()
        {
            const string scenePath = "Assets/_Project/Scenes/Main.unity";
            if (AssetDatabase.LoadAssetAtPath<SceneAsset>(scenePath) != null)
            {
                EditorUtility.DisplayDialog("SebraeLab", "Main.unity já existe. Renomeie a cena para gerar outra.", "OK");
                return;
            }
            if (!EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo()) return;
            var scene = EditorSceneManager.NewScene(NewSceneSetup.EmptyScene, NewSceneMode.Single);
            var floor = GameObject.CreatePrimitive(PrimitiveType.Plane);
            floor.name = "Chao";
            var group = new GameObject("Cubos");
            var cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
            cube.name = "CuboInterativo";
            cube.transform.SetParent(group.transform);
            cube.transform.position = new Vector3(-0.8f, 1f, 3f);
            cube.transform.localScale = Vector3.one * 0.6f;
            cube.AddComponent<ColorToggle>();
            const string prefabPath = "Assets/_Project/Prefabs/CuboInterativo.prefab";
            var prefab = PrefabUtility.SaveAsPrefabAsset(cube, prefabPath);
            Object.DestroyImmediate(cube);
            for (int i = 0; i < 2; i++)
            {
                var instance = (GameObject)PrefabUtility.InstantiatePrefab(prefab);
                instance.transform.SetParent(group.transform);
                instance.transform.position = new Vector3(i == 0 ? -0.8f : 0.8f, 1f, 3f);
            }
            var light = new GameObject("LuzDirecional").AddComponent<Light>();
            light.type = LightType.Directional;
            light.intensity = 1f;
            light.transform.rotation = Quaternion.Euler(50, -30, 0);
            RenderSettings.ambientLight = new Color(0.35f, 0.35f, 0.35f);
            var rig = new GameObject("XR Origin");
            var origin = rig.AddComponent<XROrigin>();
            origin.Origin = rig;
            origin.RequestedTrackingOriginMode = XROrigin.TrackingOriginMode.Floor;
            var offset = new GameObject("Camera Offset");
            offset.transform.SetParent(rig.transform, false);
            origin.CameraFloorOffsetObject = offset;
            var cameraObject = new GameObject("Main Camera");
            cameraObject.tag = "MainCamera";
            cameraObject.transform.SetParent(offset.transform, false);
            cameraObject.transform.localPosition = new Vector3(0, 1.6f, 0);
            var camera = cameraObject.AddComponent<Camera>();
            camera.nearClipPlane = 0.05f;
            camera.farClipPlane = 100f;
            cameraObject.AddComponent<AudioListener>();
            origin.Camera = camera;
            AddPose(cameraObject, "<XRHMD>/centerEyePosition", "<XRHMD>/centerEyeRotation");
            AddController(offset.transform, true);
            AddController(offset.transform, false);
            EditorSceneManager.SaveScene(scene, scenePath);
            EditorBuildSettings.scenes = new[] { new EditorBuildSettingsScene(scenePath, true) };
            AssetDatabase.SaveAssets();
            Selection.activeGameObject = rig;
            Debug.Log("Cena criada. Configure OpenXR e o perfil do controle em Project Settings antes de testar em VR.");
        }
        private static void AddPose(GameObject target, string position, string rotation)
        {
            var driver = target.AddComponent<TrackedPoseDriver>();
            driver.positionInput = new InputActionProperty(new InputAction("Position", InputActionType.Value, position, expectedControlType: "Vector3"));
            driver.rotationInput = new InputActionProperty(new InputAction("Rotation", InputActionType.Value, rotation, expectedControlType: "Quaternion"));
        }
        private static void AddController(Transform parent, bool left)
        {
            string hand = left ? "LeftHand" : "RightHand";
            var controller = new GameObject(hand);
            controller.transform.SetParent(parent, false);
            string device = "<XRController>{" + hand + "}";
            AddPose(controller, device + "/devicePosition", device + "/deviceRotation");
            var beam = controller.AddComponent<LineRenderer>();
            beam.positionCount = 2;
            beam.startWidth = beam.endWidth = 0.008f;
            beam.useWorldSpace = true;
            beam.sharedMaterial = AssetDatabase.LoadAssetAtPath<Material>("Assets/_Project/Materials/Pointer.mat");
            if (beam.sharedMaterial == null)
            {
                var material = new Material(Shader.Find("Sprites/Default"));
                AssetDatabase.CreateAsset(material, "Assets/_Project/Materials/Pointer.mat");
                beam.sharedMaterial = material;
            }
            beam.startColor = beam.endColor = Color.cyan;
            controller.AddComponent<ControllerPointer>().Configure(left);
        }
    }
}