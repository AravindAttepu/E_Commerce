// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.




async function searchproducts(searchterm)
{
  
window.location = `/Products?Search=${searchterm}`; 
}


function showLoading() {
    document.getElementById("loading").style.display = "flex";
}

// Function to hide loading animation
function hideLoading() {
    document.getElementById("loading").style.display = "none";
}

function showAlert(message) {
    const container = document.getElementById('alertContainer');

    // Create alert div
    const alertDiv = document.createElement('div');
    alertDiv.className = 'alert-popup show';
    alertDiv.innerHTML = `
        <p> ${message}</p>
        <button class="close-alert" onclick="closeAlert(this)">×</button>
    `;

    // Append alert to container
    container.appendChild(alertDiv);

    // Auto-remove alert after 5 seconds
    setTimeout(() => {
        closeAlert(alertDiv);
    }, 5000);
}

function closeAlert(element) {
    // Remove alert with fade-out effect
    element.style.opacity = '0';
    setTimeout(() => element.remove(), 300);
}