import sqlite3

databaseName = "IOTMeterData.db"
conn = sqlite3.connect('..\Database\IOTMeterData.db', check_same_thread=False)

c = conn.cursor()

def GetAll():
    c.execute("SELECT * FROM IOTMeterData")
    returnValue = c.fetchall()
    conn.commit()
    return returnValue

def Get(id):
    c.execute("SELECT * FROM IOTMeterData WHERE ID=:id", {"id": id})
    returnValue = c.fetchone()
    conn.commit()
    return list(returnValue)

def Create(reading):
    c.execute("""INSERT INTO IOTMeterData ("Temperature(C)", "pH", "Turbidity(NTU)", "BOD(mg/l)", "FecalColiform(MPN/100ml)", "DisolvedOxygen(mg/l)", "NITRATENANN+NITRITENANN(mg/l)", "Conductivity(micro_mhos/cm)")
              VALUES (:temperature, :ph, :turbidity, :BOD, :fecal, :oxygen, :nitr, :conductivity)""", reading)
    conn.commit()
    return reading


def Update(reading):
    c.execute('''
        UPDATE IOTMeterData
        SET "Temperature(C)" = :temperature,
            "pH" = :ph,
            "Turbidity(NTU)" = :turbidity,
            "BOD(mg/l)" = :BOD,
            "FecalColiform(MPN/100ml)" = :fecal,
            "DisolvedOxygen(mg/l)" = :oxygen,
            "NITRATENANN+NITRITENANN(mg/l)" = :nitr,
            "Conductivity(micro_mhos/cm)" = :conductivity
        WHERE ID = :id
    ''', reading)
    conn.commit()
    return reading

def Delete(id):
    c.execute("DELETE FROM IOTMeterData WHERE ID=:id", {"id": id})
    conn.commit()
    return "Succssus"

# IoTReadings = [
#     {"id": 1, "temperature" : 24.0, "ph": 3.0,"turbidity" : 5.0, "BOD": 15.0,"fecal" : 20.0, "oxygen": 50.0,"nitr": 100.0, "conductivity" : 150.0},
#     {"id": 2, "temperature" : 25.0, "ph": 4.0,"turbidity" : 6.0, "BOD": 16.0,"fecal" : 21.0, "oxygen": 51.0,"nitr": 101.0, "conductivity" : 151.0},
#     {"id": 3, "temperature" : 26.0, "ph": 5.0,"turbidity" : 7.0, "BOD": 17.0,"fecal" : 22.0, "oxygen": 52.0,"nitr": 102.0, "conductivity" : 152.0},
#     {"id": 4, "temperature" : 26.0, "ph": 5.0,"turbidity" : 7.0, "BOD": 17.0,"fecal" : 22.0, "oxygen": 52.0,"nitr": 102.0, "conductivity" : 152.0}
# ]