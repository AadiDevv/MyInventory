import { getCurrentUser } from "../shared/api/GET_account";

export async function handleConnectionNavOutput(){
    const ulTag = document.getElementById("connection-ul");

    const user = await getCurrentUser();
    if (user) {
        ulTag.innerHTML = `
        <p>${user.username}</p>
        <form action="/Account/Logout" method="post" style="display:inline;">
            <button type="submit" class="btn text-danger">Logout</button>
        </form>
    `;
    } else {
        ulTag.innerHTML = `
        <li class="nav-item">
            <a class="nav-link btn btn-outline-primary me-2" href="/Account/Login">Login</a>
        </li>
        <li class="nav-item">
            <a class="nav-link btn btn-primary text-light" href="/Account/Register">Register</a>
        </li>
    `;
    }
}
