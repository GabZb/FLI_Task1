public static class GlobalParameters
{
    private static bool showTrajectory;
    private static bool addVelocity;

    private static float emitterVelocity;

    private static bool keyControl;


    public static bool ShowTrajectory
    {
        get { return showTrajectory; }
        set { showTrajectory = value; }
    }

    public static bool AddVelocity
    {
        get { return addVelocity; }
        set { addVelocity = value; }
    }

    public static float EmitterVelocity
    {
        get { return emitterVelocity; }
        set { emitterVelocity = value; }
    }

    public static bool KeyControl
    {
        get { return keyControl; }
        set { keyControl = value; }
    }
}


