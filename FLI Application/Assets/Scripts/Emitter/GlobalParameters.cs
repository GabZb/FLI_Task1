public static class GlobalParameters
{

    // class variables

    private static bool showTrajectory;
    private static bool addVelocity;

    private static float emitterVelocity;

    private static bool keyControl;


    //  properties = extension of class variable and it provides a mechanism to
    // read, write or change the value of class variable without effecting the
    // external way of accessing it in our applications.

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


