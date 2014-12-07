using UnityEngine;
using System.Collections;

[RequireComponent(typeof(DialogBox))]
public class DishDelivery : MonoBehaviour
{
	public Kitchen Kitchen;
	public DialogBox DragInstructionsPrefab;
	public ContentHandler InstructionsHolder;

	private DialogBox _box;
	private Food _dish;

	private DialogBox _activeInstructions;

	void Awake()
	{
		_box = GetComponent<DialogBox>();
	}

	public void DisplayDelivery(Food dish)
	{
		_dish = dish;

		_activeInstructions = (DialogBox)Object.Instantiate(DragInstructionsPrefab, new Vector3(70, -60, 0), new Quaternion());
		//instructions.gameObject.transform.parent = InstructionsHolder.transform;

		_activeInstructions.TransitionIn();

		_box.TransitionIn();
	}

	public void DeliverFood(Table table)
	{
		table.DeliverDish(_dish);
		_activeInstructions.TransitionOut();
		_box.TransitionOut();

		var destroy = _activeInstructions.gameObject.AddComponent<DestroyAfterTime>();
		destroy.Time = 2;
		destroy.ObjectToDestroy = _activeInstructions.gameObject;

		destroy = _box.gameObject.AddComponent<DestroyAfterTime>();
		destroy.Time = 2;
		destroy.ObjectToDestroy = _box.gameObject;

		_box = null;
		_activeInstructions = null;
		GameObject.FindGameObjectWithTag("Kitchen").GetComponent<Kitchen>().ActivateButton();
	}
}
