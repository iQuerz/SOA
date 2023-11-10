from flask import Flask, jsonify, request
from data import IoTReadings
app = Flask(__name__)


#Route get all Readings
@app.route('/iots', methods=['GET'])
def get_iots():
    return IoTReadings

#Route Get spcific Reading
@app.route('/iots/<int:iot_id>', methods=['GET'])
def get_reading(iot_id):
    for reading in IoTReadings:
        if reading['id']==iot_id:
            return reading
        
    return {'error':'IoT not found'}

#Route Crate Readings
@app.route('/iots', methods=['POST'])
def create_reading():
    new_reading={'id':len(IoTReadings)+1,
                 'temperature':request.json['temperature'], 
                 'ph':request.json['ph'],
                 'turbidity':request.json['turbidity'],
                 'BOD':request.json['BOD'],
                 'fecal':request.json['fecal'],
                 'oxygen':request.json['oxygen'],
                 'nitr':request.json['nitr'],
                 'conductivity':request.json['conductivity']}
    IoTReadings.append(new_reading)
    return new_reading

#Route Update a Readings
@app.route('/iots/<int:iot_id>', methods=['PUT'])
def update_book(iot_id):
    for reading in IoTReadings:
        if reading['id']==iot_id:
            reading['temperature']=request.json['temperature']
            reading['ph']=request.json['ph']
            reading['turbidity']=request.json['turbidity']
            reading['BOD']=request.json['BOD']
            reading['fecal']=request.json['fecal']
            reading['oxygen']=request.json['oxygen']
            reading['nitr']=request.json['nitr']
            reading['conductivity']=request.json['conductivity']
            return reading 
        
    return {'error':'Reading not found'}

#Route Delete a Readings
@app.route('/iots/<int:iot_id>', methods=['DELETE'])
def delete_book(iot_id):
    for reading in IoTReadings:
        if reading['id']==iot_id:
            IoTReadings.remove(reading)
            return {"data":"Reading Deleted Successfully"}

    return {'error':'Reading not found'}


#Runing
if __name__ == '__main__':
    app.run(debug=True)