<template>
    <nav class="navbar navbar-expand-lg navbar-light bg-info">
        <router-link class="navbar-brand" :to="routes[0].path">
            <font-awesome-icon icon="shopping-bag"/>&nbsp;Modern shopping
        </router-link>
        <button
            class="navbar-toggler"
            type="button"
            data-toggle="collapse"
            data-target="#navbarSupportedContent"
            aria-controls="navbarSupportedContent"
            aria-expanded="false"
            aria-label="Toggle navigation"
        >
            <span class="navbar-toggler-icon"></span>
        </button>

        <div class="collapse navbar-collapse" id="navbarSupportedContent">
            <ul class="navbar-nav mr-auto">
                <router-link
                    tag="li"
                    class="nav-item"
                    active-class="active"
                    exact
                    :to="route.path"
                    v-for="route in routes"
                    :key="route.name"
                >
                    <a class="nav-link" href="#">{{ route.display }}</a>
                </router-link>
            </ul>
            <form class="form-inline my-2 my-lg-0">
                <input
                    class="form-control mr-sm-2"
                    type="search"
                    placeholder="Search"
                    aria-label="Search"
                >
                <button class="btn btn-outline-secondary my-2 my-sm-0" type="submit">Search</button>
            </form>
        </div>
    </nav>
</template>

<script>
import { routes } from "../router/routes";

export default {
    data() {
        return {
            routes
        };
    }
};
</script>

<style scoped>
</style>
