<template>
    <div class="LogBg">
        <Login/>
    </div>
</template>

<style scoped>
.LogBg{
    width: 100vw;
    height: 100vh;
    background: url("../assets/img/bg2.png");
    background-repeat: no-repeat;
    background-size: cover;
    background-attachment: fixed;
}
</style>

<script lang="ts">
import { Component, Vue } from 'vue-property-decorator';
import Login from '@/components/Login.vue'; // @ is an alias to /src
import axios from 'axios';

@Component({
  components: {
    Login,
  },
})
export default class Log extends Vue {

}
</script>
