using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace SuperPupSystems.GamePlay2D
{
    public class PlatformerGumba2D : CharacterController2D
    {
        public float speed = 10.0f;
        public float collisionTestOffset;
        public SpriteRenderer spriteRenderer;
        public Rigidbody2D rigidbody2D;
        public Vector2 direction = Vector2.right;
        public bool startRight = true;
        public int damage = 2;
        public List<string> tagsToDamage;
        
        void Start()
        {
            rigidbody2D = GetComponent<Rigidbody2D>();
        }

        
        void Update()
        {
            Vector2 motion = rigidbody2D.velocity;
            isTouchingGround = IsTouchingGround();

            if (isTouchingGround)
            {
                // Attack
                List<RaycastHit2D> rightHits = new List<RaycastHit2D>();
                List<RaycastHit2D> leftHits = new List<RaycastHit2D>();
                if (!TestMove(Vector2.right, collisionTestOffset, tagsToDamage, rightHits) ||
                !TestMove(Vector2.left, collisionTestOffset, tagsToDamage, leftHits))
                {
                    List<RaycastHit2D> results = rightHits.Concat(leftHits).ToList();

                    foreach(RaycastHit2D hit in results)
                    {
                        SuperPupSystems.Helper.Health hitHealth =
                            hit.collider.gameObject.GetComponent<SuperPupSystems.Helper.Health>();
                        
                        if (hitHealth)
                        {
                            hitHealth.Damage(damage);
                        }
                    }
                }

                // Should I change direction
                if (!TestMove(direction, collisionTestOffset))
                {
                    direction *= -1;
                }

                // Movement
                rigidbody2D.velocity = direction * speed;
            }
        }
    }
}