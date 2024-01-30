using UnityEngine;
using TSS;
using System.Collections;
using System.Collections.Generic;
using TMPro;


public class TelemetryStreamClient : MonoBehaviour
{

    // Tell if the user is currently scanning
    public static bool isScanning = false;

    // GPS fields
    public static int mode;
    public static string fix_status;
    public static string GPStime;
    public static float ept;
    public static float lat;
    public static float lon;
    public static float alt;
    public static float epx;
    public static float epy;
    public static float epv;
    public static float track;
    public static float speed;
    public static float climb;
    public static float eps;
    public static float epc;

    // IMU fields
    public static string user_guid;
    public static float heading;
    public static float accel_x;
    public static float accel_y;
    public static float accel_z;
    public static float gyro_x;
    public static float gyro_y;
    public static float gyro_z;
    public static float mag_x;
    public static float mag_y;
    public static float mag_z;

    // Simulation states
    public static int room_id;
    public static bool isRunning;
    public static bool isPaused;
    public static float SIMtime;
    public static string timer;
    public static float primary_oxygen;
    public static float secondary_oxygen;
    public static float suits_pressure;
    public static float sub_pressure;
    public static float o2_pressure;
    public static float o2_rate;
    public static float h2o_gas_pressure;
    public static float h2o_liquid_pressure;
    public static float sop_pressure;
    public static float sop_rate;
    public static float heart_rate;
    public static float fan_tachometer;
    public static float battery_capacity;
    public static float temperature;
    public static string battery_time_left;
    public static string o2_time_left;
    public static string h2o_time_left;
    public static float battery_percentage;
    public static float battery_output;
    public static float oxygen_primary_time;
    public static float oxygen_secondary_time;
    public static float water_capacity;

    // Simulation failures
    public static string simfail_started_at;
    public static bool o2_error;
    public static bool pump_error;
    public static bool power_error;
    public static bool fan_error;

    // UIA fields
    // public static int room_id;
    public static string uia_started_at;
    public static bool emu1_pwr_switch;
    public static bool ev1_supply_switch;
    public static bool emu1_water_waste;
    public static bool emu1_o2_supply_switch;
    public static bool o2_vent_switch;
    public static bool depress_pump_switch;

    // Spec fields
    public static float SiO2;
    public static float TiO2;
    public static float Al2O3;
    public static float FeO;
    public static float MnO;
    public static float MgO;
    public static float CaO;
    public static float K2O;
    public static float P2O3;

    // Rover fields
    public static float rover_lat;
    public static float rover_lon;
    public static string navigation_status;    // either "NAVIGATING" or "NOT_NAVIGATING"
    public static float goal_lat;
    public static float goal_lon;


    // Connection fields
    private static TSSConnection tss;
    string tssUri;
    int msgCount = 0;

    // Text display (set in Unity GUI)
    public TextMeshPro evaMsgBox;


    void Start()
    {

        Debug.Log("Starting telemetry stream client...");
        tss = new TSSConnection();
        Connect();

    }


    void Update()
    {
        // Updates the websocket once per frame
        tss.Update();

    }

    public async void Connect()
    {
        tssUri = "ws://192.168.50.10:3001";
        var connecting = tss.ConnectToURI(tssUri, "Archimedes", "VK010", "Rice University", "b16f5bff-a93a-48c1-bfd9-6d74dec1b144" ); 
        Debug.Log("Connecting to " + tssUri);
        // Create a function that takes asing TSSMsg parameter and returns void. For example:
        // public static void PrintInfo(TSS.Msgs.TSSMsg tssMsg) { ... }
        // Then just subscribe to the OnTSSTelemetryMsg
        tss.OnTSSTelemetryMsg += (telemMsg) =>
        {
            msgCount++;
            Debug.Log("Message #" + msgCount + "\nMessage:\n " + JsonUtility.ToJson(telemMsg, prettyPrint: true));

            if (telemMsg.gpsMsg != null)
            {
                GPStime = telemMsg.gpsMsg.time;
                ept = telemMsg.gpsMsg.ept;
                lat = telemMsg.gpsMsg.lat;
                lon = telemMsg.gpsMsg.lon;
                alt = telemMsg.gpsMsg.alt;
                epx = telemMsg.gpsMsg.epx;
                epy = telemMsg.gpsMsg.epy;
                epv = telemMsg.gpsMsg.epv;
                track = telemMsg.gpsMsg.track;
                speed = telemMsg.gpsMsg.speed;
                climb = telemMsg.gpsMsg.climb;
                eps = telemMsg.gpsMsg.eps;
                epc = telemMsg.gpsMsg.epc;
            }

            if (telemMsg.imuMsg != null)
            {
                user_guid = telemMsg.imuMsg.user_guid;
                heading = telemMsg.imuMsg.heading;
                accel_x = telemMsg.imuMsg.accel_x;
                accel_y = telemMsg.imuMsg.accel_y;
                accel_z = telemMsg.imuMsg.accel_z;
                gyro_x = telemMsg.imuMsg.gyro_x;
                gyro_y = telemMsg.imuMsg.gyro_y;
                gyro_z = telemMsg.imuMsg.gyro_z;
                mag_x = telemMsg.imuMsg.mag_x;
                mag_y = telemMsg.imuMsg.mag_y;
                mag_z = telemMsg.imuMsg.mag_z;
            }

            if (telemMsg.simulationStates != null)
            {
                room_id = telemMsg.simulationStates.room_id;
                isRunning = telemMsg.simulationStates.isRunning;
                isPaused = telemMsg.simulationStates.isPaused;
                SIMtime = telemMsg.simulationStates.time;
                timer = telemMsg.simulationStates.timer;
                primary_oxygen = telemMsg.simulationStates.primary_oxygen;
                secondary_oxygen = telemMsg.simulationStates.secondary_oxygen;
                suits_pressure = telemMsg.simulationStates.suits_pressure;
                sub_pressure = telemMsg.simulationStates.sub_pressure;
                o2_pressure = telemMsg.simulationStates.o2_pressure;
                o2_rate = telemMsg.simulationStates.o2_rate;
                h2o_gas_pressure = telemMsg.simulationStates.h2o_gas_pressure;
                h2o_liquid_pressure = telemMsg.simulationStates.h2o_liquid_pressure;
                sop_pressure = telemMsg.simulationStates.sop_pressure;
                sop_rate = telemMsg.simulationStates.sop_rate;
                heart_rate = telemMsg.simulationStates.heart_rate;
                fan_tachometer = telemMsg.simulationStates.fan_tachometer;
                battery_capacity = telemMsg.simulationStates.battery_capacity;
                temperature = telemMsg.simulationStates.temperature;
                battery_time_left = telemMsg.simulationStates.battery_time_left;
                o2_time_left = telemMsg.simulationStates.o2_time_left;
                h2o_time_left = telemMsg.simulationStates.h2o_time_left;
                battery_percentage = telemMsg.simulationStates.battery_percentage;
                battery_output = telemMsg.simulationStates.battery_output;
                oxygen_primary_time = telemMsg.simulationStates.oxygen_primary_time;
                oxygen_secondary_time = telemMsg.simulationStates.oxygen_secondary_time;
                water_capacity = telemMsg.simulationStates.water_capacity;
            }

            if (telemMsg.simulationFailures != null)
            {
                simfail_started_at = telemMsg.simulationFailures.started_at;
                o2_error = telemMsg.simulationFailures.o2_error;
                pump_error = telemMsg.simulationFailures.pump_error;
                power_error = telemMsg.simulationFailures.power_error;
                fan_error = telemMsg.simulationFailures.fan_error;
            }

            if (telemMsg.uiaMsg != null)
            {
                uia_started_at = telemMsg.uiaMsg.started_at;
                emu1_pwr_switch = telemMsg.uiaMsg.emu1_pwr_switch;
                ev1_supply_switch = telemMsg.uiaMsg.ev1_supply_switch;
                emu1_water_waste = telemMsg.uiaMsg.emu1_water_waste;
                emu1_o2_supply_switch = telemMsg.uiaMsg.emu1_o2_supply_switch;
                o2_vent_switch = telemMsg.uiaMsg.o2_vent_switch;
                depress_pump_switch = telemMsg.uiaMsg.depress_pump_switch;
            }

            if (telemMsg.specMsg != null)
            {
                isScanning = true;
                SiO2 = telemMsg.specMsg.SiO2;
                TiO2 = telemMsg.specMsg.TiO2;
                Al2O3 = telemMsg.specMsg.Al2O3;
                FeO = telemMsg.specMsg.FeO;
                MnO = telemMsg.specMsg.MnO;
                MgO = telemMsg.specMsg.MgO;
                CaO = telemMsg.specMsg.CaO;
                K2O = telemMsg.specMsg.K2O;
                P2O3 = telemMsg.specMsg.P2O3;
            }
            else
            {
                isScanning = false;
            }

            if (telemMsg.roverMsg != null)
            {
                lat = telemMsg.roverMsg.lat;
                lon = telemMsg.roverMsg.lon;
                navigation_status = telemMsg.roverMsg.navigation_status;
                goal_lat = telemMsg.roverMsg.goal_lat;
                goal_lon = telemMsg.roverMsg.goal_lon;
            }



        };

        // tss.OnOpen, OnError, and OnClose events just re-raise events from websockets.
        // Similar to OnTSSTelemetryMsg, create functions with the appropriate return type and parameters, and subscribe to them
        tss.OnOpen += () =>
        {
            Debug.Log("Websocket connectio opened");
        };

        tss.OnError += (string e) =>
        {
            Debug.Log("Websocket error occured: " + e);
        };

        tss.OnClose += (e) =>
        {
            Debug.Log("Websocket closed with code: " + e);
        };

        await connecting;

    }


    /*
     * FOR USE BY THE GPSHANDLER ONLY
     * 
     * The telemetry stream client should be the only thing on this side that has access
     * to the TSS. 
     */
    public static void DeployRover(float cone_lat, float cone_lon)
    {
        tss.SendRoverNavigateCommand(cone_lat, cone_lon);
    }

    public static void RecallRover()
    {
        tss.SendRoverRecallCommand();
    }

}