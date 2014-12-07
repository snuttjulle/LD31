using UnityEngine;
using System.Collections;

[RequireComponent(typeof(DialogBox))]
public class DishDelivery : MonoBehaviour
{
	public DialogBox DragInstructionsPrefab;
	public ContentHandler InstructionsHolder;

	private DialogBox _box;
	private Food _dish;

	void Awake()
	{
		_box = GetComponent<DialogBox>();
	}

	public void DisplayDelivery(Food dish)
	{
		_dish = dish;

		var instructions = (DialogBox)Object.Instantiate(DragInstructionsPrefab, new Vector3(70, -60, 0), new Quaternion());
		//instructions.gameObject.transform.parent = InstructionsHolder.transform;

		instructions.TransitionIn();

		_box.TransitionIn();
	}

	public void DeliverFood(Table table)
	{
		table.DeliverDish(_dish);
	}
}
