
function showData() {
    const from = document.getElementById('from').value+'T12:00:00';
    const to = document.getElementById('to').value+'T12:00:00';

    console.log(`Fetching data from ${from} to ${to}`);

    fetch(`http://69.90.132.97:8666/api/growdata?from=${from}&to=${to}`)
        .then(response => response.json())
        .then(growData => {
            var tmpCtx = document.getElementById('temperatureChart').getContext('2d');
            var humCtx = document.getElementById('humidityChart').getContext('2d');
            var tmpChart = createTemperatureChart(tmpCtx, growData);
            var humChart = createHumidityChart(humCtx, growData);
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
            labels: growData.map(createDateTimeLabel),
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
            labels: growData.map(createDateTimeLabel),
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

/**
 * Creates datetime label for given data item.
 * 
 * @param {Object} dataItem Data item to create datetime label for.
 */
function createDateTimeLabel(dataItem) {
    return dataItem["timestamp"].substr(0,10);
}