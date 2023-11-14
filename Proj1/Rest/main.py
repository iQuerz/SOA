from flask import Flask, jsonify, request
from data import GetAll,Get,Create,Update,Delete
#from flask_swagger_ui import get_swaggerui_blueprint
#from flask_restplus import Api, Resource, fields
from flasgger import Swagger

app = Flask(__name__)
swagger = Swagger(app)


@app.route('/iots', methods=['GET'])
def get_iots():
    """
    Get all IoT readings
    ---
    responses:
      200:
        description: Return all IoT readings
    """
    return GetAll()

#Route Get spcific Reading
@app.route('/iots/<int:iot_id>', methods=['GET'])
def get_reading(iot_id):
    """
    Get a specific IoT reading by ID
    ---
    parameters:
      - in: path
        name: iot_id
        required: true
        schema:
          type: integer
    responses:
      200:
        description: Return the specified IoT reading
      404:
        description: IoT reading not found
    """
    return Get(iot_id)
        

#Route Crate Readings
#use this for body V
#{ "temperature" : 29.0, "ph": 9.0,"turbidity" : 9.0, "BOD": 18.0,"fecal" : 28.0, "oxygen": 58.0,"nitr": 108.0, "conductivity" : 158.0}
@app.route('/iots', methods=['POST'])
def create_reading():
    """
    Create a new IoT reading
    ---
    parameters:
      - in: body
        name: body
        required: true
        schema:
          type: object
          properties:
            temperature:
              type: number
            ph:
              type: number
            turbidity:
              type: number
            BOD:
              type: number
            fecal:
              type: number
            oxygen:
              type: number
            nitr:
              type: number
            conductivity:
              type: number
        example:
          temperature: 29.0
          ph: 9.0
          turbidity: 9.0
          BOD: 18.0
          fecal: 28.0
          oxygen: 58.0
          nitr: 108.0
          conductivity: 158.0
    responses:
      201:
        description: Successfully created a new IoT reading
      400:
        description: Bad Request - Invalid input data
    """
    new_reading={'temperature':request.json['temperature'], 
                 'ph':request.json['ph'],
                 'turbidity':request.json['turbidity'],
                 'BOD':request.json['BOD'],
                 'fecal':request.json['fecal'],
                 'oxygen':request.json['oxygen'],
                 'nitr':request.json['nitr'],
                 'conductivity':request.json['conductivity']}
    return Create(new_reading)

#Route Update a Readings
@app.route('/iots/<int:iot_id>', methods=['PUT'])
def update_book(iot_id):
    """
    Update a specific IoT reading by ID
    ---
    parameters:
      - in: body
        name: body
        required: true
        schema:
          type: object
          properties:
            temperature:
              type: number
            ph:
              type: number
            turbidity:
              type: number
            BOD:
              type: number
            fecal:
              type: number
            oxygen:
              type: number
            nitr:
              type: number
            conductivity:
              type: number
        example:
          temperature: 29.0
          ph: 9.0
          turbidity: 9.0
          BOD: 18.0
          fecal: 28.0
          oxygen: 58.0
          nitr: 108.0
          conductivity: 158.0
    responses:
      200:
        description: Successfully updated the IoT reading
      404:
        description: IoT reading not found
      400:
        description: Bad Request - Invalid input data
    """
    reading = Get(iot_id)
    if reading[0]==iot_id:
        reading[0]=request.json['temperature']
        reading[1]=request.json['ph']
        reading[2]=request.json['turbidity']
        reading[3]=request.json['BOD']
        reading[4]=request.json['fecal']
        reading[5]=request.json['oxygen']
        reading[6]=request.json['nitr']
        reading[7]=request.json['conductivity']
        reading[8]=iot_id
    return Update(reading) 
        
#Route Delete a Readings
@app.route('/iots/<int:iot_id>', methods=['DELETE'])
def delete_book(iot_id):
    """
    Delete a specific IoT reading by ID
    ---
    parameters:
      - in: path
        name: iot_id
        required: true
        schema:
          type: integer
    responses:
      204:
        description: Successfully deleted the IoT reading
      404:
        description: IoT reading not found
    """
    return Delete(iot_id)

#Runing
if __name__ == '__main__':
    app.run(debug=True)