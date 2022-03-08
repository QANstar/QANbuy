<template>
    <el-container>
            <el-aside class="leftNav" width = "200px">
                <LeftNav/>
            </el-aside>
            <el-main>
                <router-view/>
            </el-main>
        </el-container>
</template>

<script lang="ts">
import { Component, Vue } from 'vue-property-decorator';
import LeftNav from '@/components/LeftNav.vue'; // @ is an alias to /src
import axios from 'axios';

@Component({
  components: {
    LeftNav,
  },
})
export default class Home extends Vue {

}
</script>
