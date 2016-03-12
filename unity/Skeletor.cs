using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;

public class Skeletor : MonoBehaviour {

	[Tooltip("if null, the Animator will be detected by GetComponent()")]
	public Animator animator;

	public Transform hips;

	public Transform leftShoulder;
	public Transform leftUpperLeg;
	public Transform leftLowerLeg;
	public Transform leftFoot;

	public Transform rightShoulder;
	public Transform rightUpperLeg;
	public Transform rightLowerLeg;
	public Transform rightFoot;

	public Transform spine;
	public Transform chest;
	public Transform neck;
	public Transform head;

	public Transform leftUpperArm;
	public Transform leftLowerArm;
	public Transform leftHand;

	public Transform rightUpperArm;
	public Transform rightLowerArm;
	public Transform rightHand;



	[Range(0, 1)]
	[Tooltip("how much the arm can stretch in IK")]
	public float armStretch = 0.05f;
	[Range(0, 1)]
	[Tooltip("minimal space between feets")]
	public float feetSpacing = 0;
	[Range(0, 1)]
	[Tooltip("how much the leg can stretch in IK")]
	public float legStretch = 0.05f;

	[Range(-1, 1)]
	public float lowerArmTwist = 0.5f;
	[Range(-1, 1)]
	public float lowerLegTwist = 0.5f;

	[Range(-1, 1)]
	public float upperArmTwist = 0.5f;
	[Range(-1, 1)]
	public float upperLegTwist = 0.5f;

	private List<HumanBone> bones;
	private List<SkeletonBone> sbones;

	private void AddBone(string name, Transform t) {


		HumanBone bone = new HumanBone();
		bone.humanName = name;
		bone.boneName = t.name;
		bone.limit = new HumanLimit();
		bone.limit.useDefaultValues = true;

		bones.Add(bone);

		SkeletonBone sbone = new SkeletonBone();
		sbone.name = t.name;

		sbone.position = t.localPosition;
		sbone.rotation = t.localRotation;
		sbone.scale = t.localScale;

		sbones.Add(sbone);

	}

	// Use this for initialization
	void Start() {

		if (animator == null) {
			animator = GetComponent<Animator>();
		}
		

		bones = new List<HumanBone>();
		sbones = new List<SkeletonBone>();

		HumanDescription desc = new HumanDescription();

		desc.armStretch = armStretch;
		desc.feetSpacing = feetSpacing;
		desc.legStretch = legStretch;
		desc.lowerArmTwist = lowerArmTwist;
		desc.lowerLegTwist = lowerLegTwist;
		desc.upperArmTwist = upperArmTwist;
		desc.upperLegTwist = upperLegTwist;

		AddBone("Hips", hips);
		AddBone("LeftUpperLeg", leftUpperLeg);
		AddBone("RightUpperLeg", rightUpperLeg);

		AddBone("LeftLowerLeg", leftLowerLeg);
		AddBone("RightLowerLeg", rightLowerLeg);

		AddBone("LeftFoot", leftFoot);
		AddBone("RightFoot", rightFoot);


		AddBone("Spine", spine);

		if (chest != null) {
			AddBone("Chest", chest);
		}

		if (neck != null) {
			AddBone("Neck", neck);
		}

		AddBone("Head", head);


		if (leftShoulder != null) {
			AddBone("LeftShoulder", leftShoulder);
		}
		AddBone("LeftUpperArm", leftUpperArm);

		if (rightShoulder != null) {
			AddBone("RightShoulder", rightShoulder);
		}
		AddBone("RightUpperArm", rightUpperArm);



		AddBone("LeftLowerArm", leftLowerArm);
		AddBone("RightLowerArm", rightLowerArm);



		AddBone("LeftHand", leftHand);
		AddBone("RightHand", rightHand);

		AddBone("Root", transform);


		desc.human = bones.ToArray();
		desc.skeleton = sbones.ToArray();

		Avatar avatar = AvatarBuilder.BuildHumanAvatar(gameObject, desc);
		avatar.name = "Skeletor Auto Avatar";

		animator.avatar = avatar;


	}
	
	// Update is called once per frame
	void Update() {
	
	}
}
