﻿var map;

function loadMap() {
    // Provide your access token
    L.mapbox.accessToken = 'pk.eyJ1IjoieHRlY2hkZXYiLCJhIjoiY2lmNjZrdXBhMDNqanN1a3QweDByYTR5cyJ9.LR9aYogSA7MQtP7XuUFmLQ';
    // Create a map in the div #map
    map = L.mapbox.map('map', 'xtechdev.cif66kt840au8rtktyynwpzya', {
        minZoom: 4,
        maxZoom: 17,
        legendControl: {
            position: 'topright'
        }
    }).setView([1.31537, 103.81222], 16).on('ready', function () {
        new L.Control.MiniMap(L.mapbox.tileLayer('mapbox.streets'))
            .addTo(map);
    });


    var click = document.getElementById('click'),
        mousemove = document.getElementById('mousemove');

    map.on('mousemove click', function (e) {
        window[e.type].innerHTML = e.containerPoint.toString() + ', ' + e.latlng.toString();
    });

    //return myMap;
}

function bindClickToConsole() {
    console.log('binded');
    map.featureLayer.eachLayer(function (layer) {
        layer.on('click', function (e) {
            console.log('****click start****');
            map.panTo(e.latlng);
            console.log(e);
            //console.log(e.target.feature.id);
            //console.log(e.target.feature.type);
            //console.log(e.target.feature.geometry.type);
            //console.log(e.target.feature.geometry.coordinates);
            //console.log(e.target.feature.properties.title);
            //console.log(e.target.feature.properties.fill);
            //console.log(e.target.feature.properties["marker-color"]);
            //console.log(e.latlng.lat);
            //console.log(e.latlng.lng);
            console.log('****click end****');
        });
    });
}

var mk = new Array();
var ws;

function connectsocket() {
    if (ws == null) {
        startsocket();
    } else {
        ws.open();
    }
}

function startsocket() {
    var wsImpl = window.WebSocket || window.MozWebSocket;
    if (!wsImpl) {
        alert("Browser not supported");
        return;
    }
    
    // create a new websocket and connect
    ws = new wsImpl('ws://127.0.0.1:8181/');

    // when data is comming from the server, this metod is called
    ws.onmessage = function (evt) {
        var obj = eval('(' + evt.data + ')');
        if (mk[obj.id] == null) {
            if (obj.role == "Staff") {
                mk[obj.id] = L.marker([obj.px, obj.py], {
                    icon: L.icon({
                        "iconUrl": "/img/circle_blue.png",
                        "iconSize": [16, 16], // size of the icon
                        "iconAnchor": [8, 8], // point of the icon which will correspond to marker's location
                        "popupAnchor": [0, -8], // point from which the popup should open relative to the iconAnchor
                        //"className": "dot"
                    }),

                    //    L.mapbox.marker.icon({
                    //    'marker-color': '#FFFF00',
                    //    'marker-id': obj.id,
                    //    'marker-size': 'small'
                    //}),
                    title: obj.role,
                    alt: obj.id
                });
            } else if (obj.role == "Zombie") {
                mk[obj.id] = L.marker([obj.px, obj.py], {
                    icon: L.icon({
                        "iconUrl": "/img/circle_red.png",
                        "iconSize": [16, 16], // size of the icon
                        "iconAnchor": [8, 8], // point of the icon which will correspond to marker's location
                        "popupAnchor": [0, -8], // point from which the popup should open relative to the iconAnchor
                        //"className": "dot"
                    }),
                    //    L.mapbox.marker.icon({
                    //    'marker-color': '#FF0000',
                    //    'marker-size': 'small'
                    //}),
                    title: obj.role,
                    alt: obj.id
                });
            } else {
                mk[obj.id] = L.marker([obj.px, obj.py], {
                    icon: L.icon({
                        "iconUrl": "/img/circle_green.png",
                        "iconSize": [16, 16], // size of the icon
                        "iconAnchor": [8, 8], // point of the icon which will correspond to marker's location
                        "popupAnchor": [0, -8], // point from which the popup should open relative to the iconAnchor
                        //"className": "dot"
                    }),
                    //    L.mapbox.marker.icon({
                    //    'marker-color': '#00FF00',
                    //    'marker-id': obj.id,
                    //    'marker-size': 'small'
                    //}),
                    title: obj.role,
                    alt: obj.id
                });
            }

            //one way to create marker using leaf
            //mk[obj.id] = L.marker([obj.px, obj.py], {
            //    icon: L.mapbox.marker.icon({
            //        'marker-color': '#f86767',
            //        'marker-id': obj.id,
            //        'marker-size': 'small'
            //    })
            //});

            //other way to create marker
            //mk[obj.id] = L.mapbox.featureLayer({
            //    // this feature is in the GeoJSON format: see geojson.org
            //    // for the full specification
            //    type: 'Feature',
            //    geometry: {
            //        type: 'Point',
            //        // coordinates here are in longitude, latitude order because
            //        // x, y is the standard for GeoJSON and many formats
            //        coordinates: [
            //          obj.px,
            //          obj.py
            //        ]
            //    },
            //    properties: {
            //        title: 'Peregrine Espresso',
            //        description: '1718 14th St NW, Washington, DC',
            //        // one can customize markers by adding simplestyle properties
            //        // https://www.mapbox.com/guides/an-open-platform/#simplestyle
            //        'marker-size': 'large',
            //        'marker-color': '#f86767',
            //        'marker-id': obj.id
            //        //'marker-symbol': 'cafe'
            //    }
            //}).addTo(map);

            //mk[obj.id].addEventListener("click", function () {
            //    console.log(this);
            //});
            mk[obj.id].on('click', function (e) {
                console.log('****click start****');
                map.panTo(e.latlng);
                console.log(e);
                var role = e.target._icon.title;
                var id = e.target._icon.alt;
                console.log(id);
                console.log('****click end****');
                $('.nav-tabs a[href="#info"]').tab('show');
                document.getElementById('info_content').innerHTML = "Role: " + role + "<br>Bib: #" + id + "<br>Name: Tom<br>Wave: #1<br>Contact Number: 123456<br>Age: 28<br>Gender: Male<br>Medical Info: ...<br>Emergency Contact Person: ...";
            });
            mk[obj.id].addTo(map);
            
            console.log("add");
        }
        else {
            //var latlng = mk[obj.id].getLatLng();
            //latlng.lat = obj.px;
            //latlng.lng = obj.py;
            //mk[obj.id].setLatLng(latlng);
            mk[obj.id].setLatLng(L.latLng(obj.px, obj.py));

            //var name = Object.keys(mk[obj.id]._layers)[0];          
            //mk[obj.id]._layers[name]._latlng.lat = obj.px;
            //mk[obj.id]._layers[name]._latlng.lng = obj.py;
            //console.log("changed");
        }

    };
}

function closesocket() {
    ws.close();
}

function addMovingPoint() {
    startsocket();
}

function removeMovingPoint() {
    closesocket();
    var i;
    for (i = 0; i < mk.length; i++) {
        if (mk[i]==null) {
            continue;
        }
        if (map.hasLayer(mk[i]))
            map.removeLayer(mk[i]);
        console.log("remove");
    }
    mk = new Array();
}

function addEmegencyPoint() {
    var marker = L.marker([1.30889, 103.80145], {
        icon: L.icon({
            "iconUrl": "/img/emergency.png",
            "iconSize": [32, 32], // size of the icon
            "iconAnchor": [16, 16], // point of the icon which will correspond to marker's location
            "popupAnchor": [0, -16], // point from which the popup should open relative to the iconAnchor
            //"className": "dot"
        })
        //    L.mapbox.marker.icon({
        //    'marker-color': '#FF0000',
        //    'marker-size': 'medium'
        //})
        ,
        title: "Emergency Call",
        alt: "20"
    });
    marker.on('click', function (e) {
        console.log('****click start****');
        map.panTo(e.latlng);
        console.log(e);
        var id = e.target._icon.alt;
        console.log(id);
        console.log('****click end****');
        $('.nav-tabs a[href="#emergency"]').tab('show');
        //e.target._icon.classList.remove('blink_me');
    });
    marker.addTo(map);

    marker._icon.classList.add('blink_me');
    
    var emergency = document.getElementById('emergency_content');
    var newItem = emergency.appendChild(document.createElement('div'));
    newItem.appendChild(document.createElement('hr'));
    var para = newItem.appendChild(document.createElement('p'));
    para.innerHTML = "Emergency Call<br>Staff Name: Judy<br>Staff Contact Number: 234567<br>Emergency Status: Active";

    var btn0 = document.createElement('button');
    btn0.appendChild(document.createTextNode("locate"));
    btn0.addEventListener('click', locateEmergencyPoint);
    var btn1 = document.createElement('button');
    newItem.appendChild(btn0);
    btn1.appendChild(document.createTextNode("Solved"));
    btn1.addEventListener('click', stopEmergencyPoint);
    //btn.on('click', removeEmegencyPoint);
    newItem.appendChild(btn1);
    var btn2 = document.createElement('button');
    btn2.appendChild(document.createTextNode("Remove"));
    btn2.addEventListener('click', removeEmergencyPoint);
    newItem.appendChild(btn2);
    newItem.appendChild(document.createElement('hr'));
}

function locateEmergencyPoint() {
    var layers = map._layers;
    for (var item in layers) {
        var marker = layers[item];
        //console.log(marker);
        if (marker.options.title == "Emergency Call") {
            map.panTo(marker.getLatLng());
        }
    }
}

function stopEmergencyPoint() {
    var layers = map._layers;
    for (var item in layers) {
        var marker = layers[item];
        //console.log(marker);
        if (marker.options.title == "Emergency Call") {
            marker._icon.classList.remove("blink_me");
        }
    }
}

function removeEmergencyPoint() {
    var layers = map._layers;
    for (var item in layers) {
        var marker = layers[item];
        //console.log(marker);
        if (marker.options.title == "Emergency Call") {
            map.removeLayer(marker);
            var emergency = document.getElementById('emergency_content');
            emergency.innerHTML = "";
        }
    }
}

function consoleMap() {
    console.log(map);
    
    
}

function blinkMedicalPoint() {
    map.featureLayer.eachLayer(function (layer) {
        //console.log(layer);
        if (layer.feature.properties.title == 'Medical Point') {
            console.log('add blink');
            map.panTo(layer.getLatLng());
            layer._icon.classList.add('blink_me');
        }
    });
}

function stopBlinking() {
    map.featureLayer.eachLayer(function (layer) {
        //console.log(layer);
        if (layer.feature.properties.title == 'Medical Point') {
            console.log('stop blink');
            map.panTo(layer.getLatLng());
            layer._icon.classList.remove('blink_me');
        }
    });
}

(function () {
    loadMap();
    var filters = document.getElementById('filter');
    map.featureLayer.on('ready', function () {
        bindClickToConsole();
        // Collect the types of symbols in this layer. you can also just
        // hardcode an array of types if you know what you want to filter on,
        // like var types = ['foo', 'bar'];
        var typesObj = {}, types = [];
        var features = map.featureLayer.getGeoJSON().features;
        for (var i = 0; i < features.length; i++) {
            typesObj[features[i].properties['title']] = true;
        }
        for (var k in typesObj) types.push(k);

        var checkboxes = [];
        // Create a filter interface.
        for (var i = 0; i < types.length; i++) {
            // Create an an input checkbox and label inside.
            var item = filters.appendChild(document.createElement('div'));
            var checkbox = item.appendChild(document.createElement('input'));
            var label = item.appendChild(document.createElement('label'));
            checkbox.type = 'checkbox';
            checkbox.id = types[i];
            checkbox.checked = true;
            // create a label to the right of the checkbox with explanatory text
            label.innerHTML = types[i];
            label.setAttribute('for', types[i]);
            // Whenever a person clicks on this checkbox, call the update().
            checkbox.addEventListener('change', update);
            checkboxes.push(checkbox);
        }

        // This function is called whenever someone clicks on a checkbox and changes
        // the selection of markers to be displayed.
        function update() {
            var enabled = {};
            // Run through each checkbox and record whether it is checked. If it is,
            // add it to the object of types to display, otherwise do not.
            for (var i = 0; i < checkboxes.length; i++) {
                if (checkboxes[i].checked) enabled[checkboxes[i].id] = true;
            }
            map.featureLayer.setFilter(function (feature) {
                // If this symbol is in the list, return true. if not, return false.
                // The 'in' operator in javascript does exactly that: given a string
                // or number, it says if that is in a object.
                // 2 in { 2: true } // true
                // 2 in { } // false
                return (feature.properties['title'] in enabled);
            });
        }
    });
    //bindClickToConsole();
})();