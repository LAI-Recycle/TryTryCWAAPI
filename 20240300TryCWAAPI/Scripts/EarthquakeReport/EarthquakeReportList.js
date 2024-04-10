//-------------照片縮放-------------//
function showZoomedImage(img) {
    // 創建放大的圖片元素
    var zoomedImage = document.createElement("img");
    zoomedImage.src = img.src;
    zoomedImage.alt = "Zoomed Image";
    zoomedImage.style.maxWidth = "80%";
    zoomedImage.style.maxHeight = "80%";

    // 創建關閉按鈕元素
    var closeButton = document.createElement("span");
    closeButton.textContent = "X";
    closeButton.style.color = "white";
    closeButton.style.cursor = "pointer";
    closeButton.style.fontSize = "24px";
    closeButton.style.position = "absolute";
    closeButton.style.top = "10px";
    closeButton.style.right = "20px";

    // 創建容器元素
    var zoomedImageContainer = document.createElement("div");
    zoomedImageContainer.style.display = "flex";
    zoomedImageContainer.style.justifyContent = "center";
    zoomedImageContainer.style.alignItems = "center";
    zoomedImageContainer.style.position = "fixed";
    zoomedImageContainer.style.top = "0";
    zoomedImageContainer.style.left = "0";
    zoomedImageContainer.style.width = "100%";
    zoomedImageContainer.style.height = "100%";
    zoomedImageContainer.style.background = "rgba(0, 0, 0, 0.5)";
    zoomedImageContainer.style.zIndex = "9999";

    // 將元素添加到容器中
    zoomedImageContainer.appendChild(zoomedImage);
    zoomedImageContainer.appendChild(closeButton);

    // 將容器添加到 body 中
    document.body.appendChild(zoomedImageContainer);

    closeButton.onclick = function () {
        // 移除容器
        document.body.removeChild(zoomedImageContainer);
    };
    zoomedImageContainer.onclick = function (event) {
        if (event.target.tagName.toLowerCase() !== "img") {
            document.body.removeChild(zoomedImageContainer);
        }
    };
}
//-------------照片縮放結束-------------//