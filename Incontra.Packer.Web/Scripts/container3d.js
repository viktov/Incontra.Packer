
function loadScript(url, callback) {
    var head = document.getElementsByTagName('head')[0];
    var script = document.createElement('script');
    script.type = 'text/javascript';
    script.src = url;
    script.onreadystatechange = callback;
    script.onreadystatechange = function () {
        if (this.readyState == 'complete') {
            callback();
        }
    }
    script.onload = callback;
    head.appendChild(script);
}

function drawBoxes(containerData) {
    for (var i = 0; i < containerData.boxes.length; i++) {
        var box = new BABYLON.Mesh.CreateBox("box", 1, scene);
        box.scaling.x = containerData.boxes[i].width;
        box.scaling.y = containerData.boxes[i].height;
        box.scaling.z = containerData.boxes[i].depth;
        box.position.x = containerData.boxes[i].x;
        box.position.y = containerData.boxes[i].y;
        box.position.z = containerData.boxes[i].z;
    }
}
function drawScene() {
        
    var canvas = document.getElementById("renderCanvas");
    var engine = new BABYLON.Engine(canvas, true);
    var scene = new BABYLON.Scene(engine);

    //scene.clearColor = new BABYLON.Color3(255, 255, 255);
    var camera = new BABYLON.ArcRotateCamera("ArcRotateCamera", 1, 0.8, 30, new BABYLON.Vector3(0, 0, 0), scene);
    camera.attachControl(canvas, false);
    var light = new BABYLON.HemisphericLight("light1", new BABYLON.Vector3(1, -1, 0), scene);
    light.intensity = 1;

    var containerData = document.getElementById("containerData").value;
    alert(containerData);
    var container = new BABYLON.Mesh.CreateBox("container", 10, scene);

    drawBoxes(containerData);

    engine.runRenderLoop(function () {
        scene.render();
    });
    
}

loadScript('http://localhost:53675/Scripts/babylon.2.0.js', drawScene);