
  
let cart = [];

function addtocart(productData) {
    // Create product object
    // let product = {
    //     ProductId: productId,
    //     ProductName: productName,
    //     Price: productPrice
    // };
    console.log(productData)
    let product = productData
   // productData[0].seller = "Default Seller";
    console.log(productData[0])
    // Add to cart array
    addtodatabase(productData[0])

    // Log to console (for testing)
  
}

async function addtodatabase(product)
{
    //  const response = await fetch('/Cart/addproduct'); // Fetch data from backend
           
    try {
        const response = await fetch("/Cart/addproduct", {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json'
            },
            body: JSON.stringify(product)
        });
        if(response.ok)
        {
            showAlert("Item Added to Cart")
        }
    
        if (!response.ok) {
           console.log(`HTTP error! Status: ${response.status}`);
        }
        if(response.status == 401)
        {
            window.location.href="/Auth/Login";
        }
    
        const data = await response.json();
        console.log(data);
    } 
    catch (error) {
        console.error("Error:", error);
    }
}


function openSidebar() {
    document.getElementById("sidebarModal").classList.add("active");
    document.getElementById("overlay").classList.add("active");
}

function closeSidebar() {
    document.getElementById("sidebarModal").classList.remove("active");
    document.getElementById("overlay").classList.remove("active");
}



async function setOrder() {
    try {
        
        const response = await fetch("/Orders/setorder", {
            method: "GET"
        });

        if (!response.ok) {
            throw new Error(`HTTP error! Status: ${response.status}`);
        }

        const data = await response.json();
        alert("Order placed successfully!");
        console.log("Order Response:", data);

        // Optionally reload or update the UI
        window.location.reload();
    } catch (error) {
        console.error("Error placing order:", error);
        alert("Failed to place the order. Please try again.");
    }
}

