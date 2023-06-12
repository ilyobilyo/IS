document.querySelectorAll("#chooseList").forEach(x => {
    x.addEventListener('click', getLists);
})

document.querySelectorAll("#addList").forEach(x => {
    x.addEventListener('click', addToList);
})

document.querySelectorAll("#buy").forEach(x => {
    x.addEventListener('click', buy);
})

async function buy(e) {
    e.preventDefault();

    let listId = document.getElementById('listId').value;

    let body = {
        shoppingListId: listId,
        productId: e.target.dataset.id
    };

    let response = await fetch("/ShoppingList/BuyProduct", {
        method: 'post',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify(body)
    });

    if (response.status == 200) {
        e.target.style.display = 'none';

        let button = document.createElement('button');
        button.disabled = true;
        button.classList.add("btn");
        button.classList.add("btn-success");

        let i = document.createElement('i');
        i.classList.add("fas");
        i.classList.add("fa-check-circle");

        button.textContent = "Purchased ";
        button.appendChild(i);

        e.target.parentElement.appendChild(button);
    }

    const data = await response.json();

    if (response.status == 400) {
        alert(data.message);
    }

}

async function addToList(e) {
    e.preventDefault();

    let body = {
        shoppingListId: e.target.parentElement.children[0].value,
        productId: e.target.dataset.id
    };

    let response = await fetch("/ShoppingList/AddToList", {
        method: 'post',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify(body)
    });

    if (response.status == 200) {
        alert("Successful added itam");
    }

    const data = await response.json();
    

    if (response.status == 400) {
        alert(data.message);
    }


}

async function getLists(e) {
    e.preventDefault();
    const response = await fetch("/ShoppingList/GetMyLists");
    const data = await response.json();

    for (var list of data) {
        let option = document.createElement('option');
        option.value = list.id;
        option.textContent = list.name;

        e.target.parentElement.parentElement.children[2].children[0].appendChild(option);
    }

    e.target.parentElement.parentElement.children[2].style.display = 'inline-block';
    e.target.parentElement.children[3].style.display = 'none';
}