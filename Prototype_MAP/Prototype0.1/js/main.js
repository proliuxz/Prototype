var map;

function loadMap() {
    // Provide your access token
    L.mapbox.accessToken = 'pk.eyJ1IjoieHRlY2hkZXYiLCJhIjoiY2lmNjZrdXBhMDNqanN1a3QweDByYTR5cyJ9.LR9aYogSA7MQtP7XuUFmLQ';
    // Create a map in the div #map
    var myMap = L.mapbox.map('map', 'xtechdev.cif66kt840au8rtktyynwpzya', {
        minZoom: 4,
        maxZoom: 17,
        legendControl: {
            position: 'topright'
        }
    }).setView([1.31537, 103.81222], 16);


    var click = document.getElementById('click'),
        mousemove = document.getElementById('mousemove');

    myMap.on('mousemove click', function (e) {
        window[e.type].innerHTML = e.containerPoint.toString() + ', ' + e.latlng.toString();
    });

    

    return myMap;
}


(function () {
    map = loadMap();
    var filters = document.getElementById('filter');
    map.featureLayer.on('ready', function () {
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
})();