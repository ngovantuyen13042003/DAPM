

document.getElementById('addItemButton').addEventListener('click', function () {
    addItem();
});

let index = 1; // Khởi tạo chỉ mục cho mặt hàng đầu tiên

function addItem() {
    const container = document.getElementById('reliefItemsContainer');
    const newItem = document.createElement('div');
    newItem.classList.add('relief-item');

    newItem.innerHTML = `
        <select name="danhmuc[${index}]">
            <option value="1">Thực phẩm</option>
            <option value="2">Nước uống</option>
            <option value="3">Nhu yếu phẩm</option>
            <option value="4">Tiền</option>
        </select>
        <input type="text" name="TenHangUngHo[${index}]" placeholder="Tên hàng ủng hộ">
        <input type="number" name="SoLuong[${index}]" placeholder="Số lượng">
        <input type="text" name="DonViTinh[${index}]" placeholder="Đơn vị tính">
    `;

    container.appendChild(newItem);
    index++; // Tăng chỉ mục cho mặt hàng tiếp theo
}

function closeForm() {
    if (confirm('Are you sure you want to close the form without saving?')) {
        window.close(); // Note: This may not work in all browsers due to security restrictions.
    }
}
