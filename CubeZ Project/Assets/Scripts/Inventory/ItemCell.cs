using System;
using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(RectTransform))]
[RequireComponent(typeof(Image))]
[RequireComponent(typeof(BoxCollider2D))]
[RequireComponent(typeof(Rigidbody2D))]
public class ItemCell : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler, IItemCell
    {
    private RectTransform rectTransform;


    private Color startedColor = Color.white;
    private Color transperentColor;


    private Image thisImage;
  [SerializeField]  private Image icon;

    private Button buttonIcon;
    private GridLayoutGroup gridLayoutGroup;

    private ItemBaseData dataTarget;


    private const string GRID_ITEMS_TAG = "GridItems";
    private const string TAG_RAM_FAST_PANRL = "RamFastPanel";

    private RectTransform targetRam;

    public event Action<ItemBaseData> onClick;


    // Use this for initialization
    void Start()
    {
        Ini();

    }

    private void Ini()
    {
        if (icon == null)
        {
            throw new ItemCellException("icon is null");
        }
        rectTransform = GetComponent<RectTransform>();
        thisImage = GetComponent<Image>();
        var alphaColor = startedColor;
        alphaColor.a = 0;
        transperentColor = alphaColor;
        if (!GameObject.FindGameObjectWithTag(GRID_ITEMS_TAG).TryGetComponent(out gridLayoutGroup))
        {
            throw new ItemCellException("grid items is null");
        }

        if (!icon.TryGetComponent(out buttonIcon))
        {
            buttonIcon = icon.gameObject.AddComponent<Button>();
        }

        buttonIcon.transition = Selectable.Transition.None;
        buttonIcon.onClick.AddListener(Select);
    }

    private void Select()
    {
        onClick?.Invoke(dataTarget);
    }

    // Update is called once per frame
    void Update()
        {

        }

    public void OnDrag(PointerEventData eventData)
    {
        rectTransform.position += (Vector3)eventData.delta;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        SetColorBorder(transperentColor);
    }

    private void SetColorBorder (Color color)
    {
        thisImage.color = color;
    }

    public void LoadIcon ()
    {
        Sprite iconSprite = dataTarget.icon;
        if (iconSprite == null)
        {
            icon.color = transperentColor;
            throw new ItemCellException("sprite icon is null");
        }
        icon.sprite = iconSprite;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        SetColorBorder(startedColor);

        if (targetRam != null)
        {
            SetToFastPanel();
        }

        else
        {
            transform.SetParent(gridLayoutGroup.transform);
            UpdateGrid(gridLayoutGroup);
        }

        if (dataTarget != null)
        {
            MarkItemFastPanelExits(targetRam != null);
        }
    }

    private void SetToFastPanel()
    {
        transform.SetParent(targetRam);
        rectTransform.position = targetRam.position;
    }

    public void SetToFastPanel(Transform target)
    {
        if (!target.TryGetComponent(out targetRam))
        {
            throw new ItemCellException("ram not valid RectTransform component");
        }
        thisImage.rectTransform.sizeDelta = targetRam.sizeDelta;
        SetToFastPanel();
    }

    private void UpdateGrid(LayoutGroup gridLayoutGroup)
    {
        gridLayoutGroup.CalculateLayoutInputHorizontal();
        gridLayoutGroup.CalculateLayoutInputVertical();
        gridLayoutGroup.SetLayoutHorizontal();
        gridLayoutGroup.SetLayoutVertical();

    }

    public void SetData (ItemBaseData data)
    {
        Ini();
        if (data == null)
        {
            throw new ItemCellException("data is null");
        }

        dataTarget = data;
        LoadIcon();
    }



    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == TAG_RAM_FAST_PANRL)
        {
            if (!collision.TryGetComponent(out targetRam))
            {
                throw new ItemCellException("ram not valid RectTransform component");
            }

            transform.SetParent(targetRam);
            dataTarget.indexFastPanel = transform.parent.transform.GetSiblingIndex();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == TAG_RAM_FAST_PANRL)
        {
            if (collision.gameObject == targetRam.gameObject)
            {
                targetRam = null;
            }
        }
    }


    private void MarkItemFastPanelExits (bool status)
    {
        bool oldStatus = dataTarget.inFastPanel;
        dataTarget.inFastPanel = status;

        if (oldStatus != dataTarget.inFastPanel)
        {
            GameCacheManager.gameCache.inventory.CallEventMarkingItem();
        }
    }
}