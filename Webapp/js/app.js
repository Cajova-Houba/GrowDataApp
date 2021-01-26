function showData() {
    var tmpCtx = document.getElementById('temperatureChart').getContext('2d');
    var humCtx = document.getElementById('humidityChart').getContext('2d');
    dataSubset = growData.filter(obj => obj["timestamp"].startsWith("2021-01-25"));
    var tmpChart = new Chart(tmpCtx, {
        type: 'line',
        data: {
            labels: dataSubset.map(d => d["timestamp"].substr(11)),
            datasets: [{
                label: "Soil temperature [C°]",
                data: dataSubset.map(d => d["soilTemperature"]),
                fill: false,
                pointRadius: 0,
                borderColor: "red"
            },{
                label: "Air temperature [C°]",
                data: dataSubset.map(d => d["airTemperature"]),
                fill: false,
                pointRadius: 0,
                borderColor: "blue"
            }]
        }
    });

    var humChart = new Chart(humCtx, {
        type: 'line',
        data: {
            labels: dataSubset.map(d => d["timestamp"].substr(11)),
            datasets: [{
                label: "Air humidity [%]",
                data: dataSubset.map(d => d["airHumidity"]),
                fill: false,
                pointRadius: 0,
                borderColor: "green"
            }]
        }
    });
}