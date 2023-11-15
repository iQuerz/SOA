import sqlite3

databaseName = "iot_telemetry_data.db"
conn = sqlite3.connect('..\Database\iot_telemetry_data.db', check_same_thread=False)

c = conn.cursor()

def GetAll():
    c.execute("SELECT * FROM iot_telemetry_data")
    returnValue = c.fetchall()
    conn.commit()
    return returnValue

def Get(ts, device):
    c.execute("SELECT * FROM iot_telemetry_data WHERE ts=:ts AND device=device", {"ts": ts,"device": device})
    returnValue = c.fetchone()
    conn.commit()
    return list(returnValue)

def Create(reading):
    c.execute("""INSERT INTO iot_telemetry_data ("ts", "device", "co", "humidity", "light", "lpg", "motion", "smoke", "temp")
              VALUES (:ts, :device, :co, :humidity, :light, :lpg, :motion, :smoke, :temp)""", reading)
    conn.commit()
    return reading



def Update(reading):
    c.execute('''
        UPDATE iot_telemetry_data
        SET
            "co" = :co,
            "humidity" = :humidity,
            "light" = :light,
            "lpg" = :lpg,
            "motion" = :motion,
            "smoke" = :smoke,
            "temp" = :temp
        WHERE ts=:ts AND device=:device
    ''', reading)
    conn.commit()
    return reading


def Delete(id):
    c.execute("DELETE FROM iot_telemetry_data WHERE ts=:ts AND device=device", {"ts": ts,"device": device})
    conn.commit()
    return "Succssus"
