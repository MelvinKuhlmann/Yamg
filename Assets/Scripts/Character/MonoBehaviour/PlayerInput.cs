using UnityEngine;

public class PlayerInput : InputComponent, IDataPersister
{
    public static PlayerInput Instance
    {
        get { return s_Instance; }
    }

    protected static PlayerInput s_Instance;


    public bool HaveControl { get { return m_HaveControl; } }

    public InputButton Pause = new InputButton(KeyCode.Escape, XboxControllerButtons.Menu);
    public InputButton Interact = new InputButton(KeyCode.I, XboxControllerButtons.Y);
    public InputButton MeleeAttack = new InputButton(KeyCode.X, XboxControllerButtons.X);
    public InputButton RangedAttack = new InputButton(KeyCode.O, XboxControllerButtons.B);
    public InputButton Jump = new InputButton(KeyCode.Space, XboxControllerButtons.A);
    public InputButton Dash = new InputButton(KeyCode.C, XboxControllerButtons.B);
    public InputAxis Horizontal = new InputAxis(KeyCode.LeftArrow, KeyCode.RightArrow, XboxControllerAxes.LeftstickHorizontal);
    public InputAxis Vertical = new InputAxis(KeyCode.UpArrow, KeyCode.DownArrow, XboxControllerAxes.LeftstickVertical);
    [HideInInspector]
    public DataSettings dataSettings;

    protected bool m_HaveControl = true;

    protected bool m_DebugMenuIsOpen = false;

    void Awake()
    {
        if (s_Instance == null)
            s_Instance = this;
        else
            throw new UnityException("There cannot be more than one PlayerInput script. The instances are " + s_Instance.name + " and " + name + ".");
    }

    void OnEnable()
    {
        if (s_Instance == null)
            s_Instance = this;
        else if (s_Instance != this)
            throw new UnityException("There cannot be more than one PlayerInput script. The instances are " + s_Instance.name + " and " + name + ".");

        PersistentDataManager.RegisterPersister(this);
    }

    void OnDisable()
    {
        PersistentDataManager.UnregisterPersister(this);

        s_Instance = null;
    }

    protected override void GetInputs(bool fixedUpdateHappened)
    {
        Pause.Get(fixedUpdateHappened, inputType);
        Interact.Get(fixedUpdateHappened, inputType);
        MeleeAttack.Get(fixedUpdateHappened, inputType);
        RangedAttack.Get(fixedUpdateHappened, inputType);
        Jump.Get(fixedUpdateHappened, inputType);
        Dash.Get(fixedUpdateHappened, inputType);
        Horizontal.Get(inputType);
        Vertical.Get(inputType);

        if (Input.GetKeyDown(KeyCode.F12))
        {
            m_DebugMenuIsOpen = !m_DebugMenuIsOpen;
        }
    }

    public override void GainControl()
    {
        m_HaveControl = true;

        GainControl(Pause);
        GainControl(Interact);
        GainControl(MeleeAttack);
        GainControl(RangedAttack);
        GainControl(Jump);
        GainControl(Dash);
        GainControl(Horizontal);
        GainControl(Vertical);
    }

    public override void ReleaseControl(bool resetValues = true)
    {
        m_HaveControl = false;

        ReleaseControl(Pause, resetValues);
        ReleaseControl(Interact, resetValues);
        ReleaseControl(MeleeAttack, resetValues);
        ReleaseControl(RangedAttack, resetValues);
        ReleaseControl(Jump, resetValues);
        ReleaseControl(Dash, resetValues);
        ReleaseControl(Horizontal, resetValues);
        ReleaseControl(Vertical, resetValues);
    }

    public void DisableMeleeAttacking()
    {
        MeleeAttack.Disable();
    }

    public void EnableMeleeAttacking()
    {
        MeleeAttack.Enable();
    }

    public void DisableRangedAttacking()
    {
        RangedAttack.Disable();
    }

    public void EnableRangedAttacking()
    {
        RangedAttack.Enable();
    }

    public void DisableDash()
    {
        Dash.Disable();
    }

    public void EnableDash()
    {
        Dash.Enable();
    }

    public DataSettings GetDataSettings()
    {
        return dataSettings;
    }

    public void SetDataSettings(string dataTag, DataSettings.PersistenceType persistenceType)
    {
        dataSettings.dataTag = dataTag;
        dataSettings.persistenceType = persistenceType;
    }

    public Data SaveData()
    {
        return new Data<bool, bool, bool>(MeleeAttack.Enabled, RangedAttack.Enabled, Dash.Enabled);
    }

    public void LoadData(Data data)
    {
        Data<bool, bool, bool> playerInputData = (Data<bool, bool, bool>)data;

        if (playerInputData.value0)
            MeleeAttack.Enable();
        else
            MeleeAttack.Disable();

        if (playerInputData.value1)
            RangedAttack.Enable();
        else
            RangedAttack.Disable();

        if (playerInputData.value2)
            Dash.Enable();
        else
            Dash.Disable();
    }

    void OnGUI()
    {
        if (m_DebugMenuIsOpen)
        {
            const float height = 100;

            GUILayout.BeginArea(new Rect(30, Screen.height - height, 200, height));

            GUILayout.BeginVertical("box");
            GUILayout.Label("Press F12 to close");

            bool meleeAttackEnabled = GUILayout.Toggle(MeleeAttack.Enabled, "Enable Melee Attack");
            bool rangeAttackEnabled = GUILayout.Toggle(RangedAttack.Enabled, "Enable Range Attack");
            bool dashEnabled = GUILayout.Toggle(Dash.Enabled, "Enable Dash");

            if (meleeAttackEnabled != MeleeAttack.Enabled)
            {
                if (meleeAttackEnabled)
                    MeleeAttack.Enable();
                else
                    MeleeAttack.Disable();
            }

            if (rangeAttackEnabled != RangedAttack.Enabled)
            {
                if (rangeAttackEnabled)
                    RangedAttack.Enable();
                else
                    RangedAttack.Disable();
            }

            if (dashEnabled != Dash.Enabled)
            {
                if (dashEnabled)
                    Dash.Enable();
                else
                    Dash.Disable();
            }
            GUILayout.EndVertical();
            GUILayout.EndArea();
        }
    }
}
