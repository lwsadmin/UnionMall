if (!window.indexedDB) {
    console.log("您的浏览器不支持indexdb");
    //window.alert();
} else {
    CreateDatabase("GoodsSpec");
}

var db;
//创建数据库
function CreateDatabase(indexDbName) {
    //调用 open 方法并传递数据库名称。如果不存在具有指定名称的数据库，则会创建该数据库
    var openRequest = indexedDB.open(indexDbName, 2);
    openRequest.onerror = function (e) {//当创建数据库失败时候的回调
        alert("创建IndexDB数据库失败: " + e.target.error.message);
    };
    openRequest.onsuccess = function (event) {
        db = event.target.result;//创建数据库成功时候，将结果给db，此时db就是当前数据库
    };
    openRequest.onupgradeneeded = function (evt) {//更改数据库，或者存储对象时候在这里处理
        db = evt.target.result;
        if (!db.objectStoreNames.contains("Spec")) {
            var objectStore = db.createObjectStore("Spec", { keyPath: 'Id'});
        }
        //创建索引，条件查询需结合索引w
    }
}

function CreatDataToDataBase() {
    var openRequest = indexedDB.open("GoodsSpec");
    openRequest.onsuccess = function (event) {
        db = openRequest.result; //创建数据库成功时候，将结果给db，此时db就是当前数据库
        var i = 0;

        $(".goodsLists").find("tr").each(function () {
            i++;
            var SpecInfo = {Id: i};
            if ($(this).find("input[name='price']").val() != undefined) { SpecInfo.Price = $(this).find("input[name='price']").val(); }
            else { return true; }
            if ($(this).find("input[name='RetailPrice']").val() != undefined) { SpecInfo.RetailPrice = $(this).find("input[name='RetailPrice']").val(); }
            if ($(this).find("input[name='Stock']").val() != undefined) { SpecInfo.Stock = $(this).find("input[name='Stock']").val(); }
            if ($(this).find("input[name='SKU']").val() != undefined) { SpecInfo.SKU = $(this).find("input[name='SKU']").val(); }

            var transaction = db.transaction('Spec', 'readwrite');//.objectStore('Spec').add(SpecInfo);
            var store = transaction.objectStore("Spec");
            var request = store.add(SpecInfo);//
            request.onsuccess = function (event) {
                console.log('数据写入成功');
            };
            request.onerror = function (event) {
                console.log('数据写入失败');
            }
        });
    }
}
function clearObjectStore() {
    var openRequest = indexedDB.open("GoodsSpec");
    openRequest.onsuccess = function (event) {
        db = openRequest.result; //创建数据库成功时候，将结果给db，此时db就是当前数据库
    }
    var transaction = db.transaction("Spec", 'readwrite');
    var store = transaction.objectStore("Spec");
    store.clear();
}
function SetInputData() {
    var openRequest = indexedDB.open("GoodsSpec");
    openRequest.onsuccess = function (event) {
        db = openRequest.result; //创建数据库成功时候，将结果给db，此时db就是当前数据库

    }
    var transaction = db.transaction("Spec", 'readwrite');
    var store = transaction.objectStore("Spec");
    var i = 0;
    store.openCursor().onsuccess = function (event) {
        var cursor = event.target.result;
        if (cursor) {
            i++;
            console.log(i);
            $(".goodsLists").find("tr:eq(" + i+")").each(function () {

                $(this).find("input[name='price']").val(cursor.value.Price);
                $(this).find("input[name='RetailPrice']").val(cursor.value.RetailPrice);
                $(this).find("input[name='Stock']").val(cursor.value.Stock);
                $(this).find("input[name='SKU']").val(cursor.value.SKU);
            });
            cursor.continue();
        } else {
            console.log('没有更多数据了！');
        }
    };



}