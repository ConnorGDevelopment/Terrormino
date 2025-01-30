//using AYellowpaper.SerializedCollections;
//using Rogue;
//using Unity.XR.CoreUtils;
//using UnityEngine;
//using UnityEngine.XR.Interaction.Toolkit;

//namespace Combat
//{
//    public class Weapon : MonoBehaviour
//    {
//        private UpgradeManager _upgradeManager;

//        private Transform _bulletSpawn;

//        private ControllerEventHandler _controllerEventHandler;

//        public GameObject BulletPrefab;

//        public StatBlock BaseStats;

//        private int _currentAmmoCount;

//        public SerializedDictionary<StatKey, float> CurrentStats => StatBlock.Sum(BaseStats.Stats, _upgradeManager.Current);

//        public int CurrentAmmoCount
//        {
//            get
//            {
//                return _currentAmmoCount;
//            }
//            set
//            {
//                _currentAmmoCount = ((value >= 0) ? ((value > StatBlock.GetInt(CurrentStats, StatKey.AmmoCapacity)) ? StatBlock.GetInt(CurrentStats, StatKey.AmmoCapacity) : value) : 0);
//            }
//        }

//        public void ReloadAmmo()
//        {
//            CurrentAmmoCount = StatBlock.GetInt(CurrentStats, StatKey.AmmoCapacity);
//        }

//        public void Start()
//        {
//            if (BulletPrefab == null)
//            {
//                Debug.Log("No Bullet Prefab Assigned", gameObject);
//            }
//            if (BaseStats == null)
//            {
//                Debug.Log("No BaseStats Assigned", gameObject);
//            }
//            if (gameObject.GetNamedChild("BulletSpawn").TryGetComponent(out Transform bulletSpawnTransform))
//            {
//                _bulletSpawn = bulletSpawnTransform;
//            }
//            else
//            {
//                Debug.Log("No Child named BulletSpawn", gameObject);
//            }
//            _upgradeManager = UpgradeManager.FindManager();
//            CurrentAmmoCount = StatBlock.GetInt(CurrentStats, StatKey.AmmoCapacity);
//        }

//        public void OnGrab(SelectEnterEventArgs ctx)
//        {
//            if (ctx.interactorObject.transform.parent.TryGetComponent(out ControllerEventHandler cte))
//            {
//                _controllerEventHandler = cte;
//                _controllerEventHandler.ImprintWeapon(gameObject);
//                _controllerEventHandler.OnActivate.AddListener(FireBullet);
//            }
//        }

//        public void OnRelease(SelectExitEventArgs _)
//        {
//            _controllerEventHandler.OnActivate.RemoveAllListeners();
//        }

//        public void OnDestroy()
//        {
//            if ((bool)_controllerEventHandler)
//            {
//                _controllerEventHandler.WeaponDestroyed.Invoke();
//            }
//        }

//        public void FireBullet()
//        {
//            Debug.Log(CurrentStats[StatKey.AmmoCapacity]);
//            if (CurrentAmmoCount > 0)
//            {
//                GameObject obj = Instantiate(BulletPrefab, _bulletSpawn.position, _bulletSpawn.transform.rotation);
//                CurrentAmmoCount--;
//                obj.GetComponent<Bullet>().Init(CurrentStats, _bulletSpawn);
//                Debug.Log(gameObject.name + " Fired Bullet", gameObject);
//            }
//        }

//        public void OnCollisionEnter(Collision collision)
//        {
//            if (collision.collider.gameObject.TryGetComponent<TerrainCollider>(out var _))
//            {
//                Destroy(gameObject, CurrentStats[StatKey.ReloadSpeed]);
//            }
//        }
//    }
//}