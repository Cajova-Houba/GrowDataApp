
function showData() {
    fetch('http://69.90.132.97:8666/api/growdata')
        .then(response => response.json())
        .then(growData => {
            var tmpCtx = document.getElementById('temperatureChart').getContext('2d');
            var humCtx = document.getElementById('humidityChart').getContext('2d');
            dataSubset = growData.filter(obj => obj["timestamp"].startsWith("2021-01-25"));
            var tmpChart = createTemperatureChart(tmpCtx, dataSubset);

            var humChart = createHumidityChart(humCtx, dataSubset);
        });
}

/**
 * Returns a Chart configuration for plotting temperature.
 * 
 * @param {*} ctx Context to draw on.
 * @param {Array} growData Data to plot.
 */
function createTemperatureChart(ctx, growData) {
    return new Chart(ctx, {
        type: 'line',
        data: {
            labels: growData.map(d => d["timestamp"].substr(11)),
            datasets: [{
                label: "Soil temperature [C°]",
                data: growData.map(d => d["soilTemperature"]),
                fill: false,
                pointRadius: 0,
                borderColor: "red"
            },{
                label: "Air temperature [C°]",
                data: growData.map(d => d["airTemperature"]),
                fill: false,
                pointRadius: 0,
                borderColor: "blue"
            }]
        }
    });
}

/**
 * Returns a Chart configuration for plotting humidity.
 * 
 * @param {*} ctx Context to draw on.
 * @param {Array} growData Data to plot.
 */
function createHumidityChart(ctx, growData) {
    return new Chart(ctx, {
        type: 'line',
        data: {
            labels: growData.map(d => d["timestamp"].substr(11)),
            datasets: [{
                label: "Air humidity [%]",
                data: growData.map(d => d["airHumidity"]),
                fill: false,
                pointRadius: 0,
                borderColor: "green"
            }]
        }
    });
}