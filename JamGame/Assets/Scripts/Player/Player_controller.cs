using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game.Humanoids;

namespace Game.Player
{
    [RequireComponent(typeof(Humanoid))]
    public class Player_controller : MonoBehaviour
    {
        internal Humanoid humanoid;

        private void Awake()
        {
            //Get
            humanoid = this.gameObject.GetComponent<Humanoid>();
        }

        private void Update()
        {
            //Move
            if ((Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0) && humanoid.Rigidbody != null)
            {
                Move(new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")));
            }

            //Attack
            if(Input.GetMouseButtonDown(0))
            {
                Melee_wimp_attack(1, 2, 1);
            }
        }

        #region Functions

        private void Move(Vector2 dir)
        {
            //Set
            humanoid.Rigidbody.velocity = dir * humanoid.MoveSpeed;
        }

        private void Melee_wimp_attack(float attack_damage, float attack_distance, float target_ammount)
        {
            Vector2 mouseDir = (Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position).normalized;
            Vector2 attack_point = new Vector2(transform.position.x, transform.position.y) + (mouseDir * attack_distance);

            Collider2D[] colls = Physics2D.OverlapBoxAll(attack_point, Vector2.one, 0);
            List<Humanoid> Humanoids_in_range = new List<Humanoid>();

            print(attack_point);

            //Filter
            if(colls.Length > 0)
            {
                foreach (Collider2D coll in colls)
                {
                    if (coll.gameObject.GetComponent<Humanoid>() && coll.gameObject != this.gameObject)
                    {
                        //Set
                        Humanoids_in_range.Add(coll.gameObject.GetComponent<Humanoid>());
                    }
                }
            }
            else
            {
                return;
            }

            
            if (Humanoids_in_range.Count < target_ammount)
            {
                //Set
                target_ammount = Humanoids_in_range.Count;
            }

            //Do damage
            for (int i = 0; i < target_ammount; i++)
            {
                Humanoids_in_range[i].Take_damage(attack_damage);
            }
        }

        #endregion
    }
}

