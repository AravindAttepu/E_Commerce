
    async function uploadImage() {
        console.log("image in process")
        showAlert("In Process")
        const fileInput = document.getElementById("imageUpload");
        const allowedTypes = ["image/png", "image/jpg", "image/jpeg"];

        if (!fileInput.files.length) {
            showAlert("Please select an image!")
            return
        }

        const file = fileInput.files[0];

        // Validate file type
        if (!allowedTypes.includes(file.type)) {
            showAlert("Invalid file type! Please upload a PNG, JPG, or JPEG image.");
            return;
        }

        // Validate file size (optional: max 2MB)
        if (file.size > 2 * 1024 * 1024) {
            showAlert("File size exceeds 2MB. Please upload a smaller image.");
            return;
        }

        // Create FormData for image upload
        const formData = new FormData();
        formData.append("file", file);
        console.log(file)

        try {
            // Step 1: Upload Image
            const uploadResponse = await fetch("/Admin/uploadimage", {
                method: "POST",
                body: formData,
            });

            const uploadData = await uploadResponse.json();
            if (!uploadResponse.ok) {
                showAlert("Image Upload Failed: " + uploadData.message);
                return;
            }

            // Get the uploaded image URL
            const imageUrl = uploadData.imageUrl;

            // Step 2: Collect Other Form Data
            const productData = {
                ProductName: document.getElementById("productName").value,
                Description: document.getElementById("description").value,
                price: parseFloat(document.getElementById("price").value),
                category: document.getElementById("category").value,
                Stock: parseInt(document.getElementById("stock").value),
                ImageUrl: imageUrl, // Use uploaded image URL
                SellerId: 2 // Example seller ID (Replace with actual logic)
            };

            // Step 3: Send Product Data to Backend
            const productResponse = await fetch("/Admin/addproduct", {
                method: "POST",
                headers: {
                    "Content-Type": "application/json",
                },
                body: JSON.stringify(productData),
            });

            const productResult = await productResponse.json();
            if (productResponse.ok) {
                showAlert("Product Added Successfully!");
            } else {
                showAlert("Failed to Add Product: " + productResult.message);
            }

        } catch (error) {
            console.error("Error:", error);
            showAlert("Something went wrong!");
        }
    }

