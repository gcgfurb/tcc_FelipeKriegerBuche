import com.vanniktech.maven.publish.SonatypeHost
import config.Config

@Suppress("DSL_SCOPE_VIOLATION") // TODO: Remove once KTIJ-19369 is fixed
plugins {
    id("android-compose-library-plugin")
    id("maven-publish")
}

android {
    namespace = "com.github.gabrielbmoro.jujubasvg"

    buildTypes {
        release {
            isMinifyEnabled = false
            proguardFiles(getDefaultProguardFile("proguard-android-optimize.txt"), "proguard-rules.pro")
        }
    }
}

publishing {
    publications {
        create<MavenPublication>("mavenJava") {
            groupId = "com.github.gabrielbmoro"
            artifactId = "jujubasvg"
            version = "1.0.0"

            afterEvaluate {
                from(components["release"])
            }

            pom {
                name.set("JujubaSVG")
                description.set("SVG handler for Android projects")
            }
        }
    }

    repositories {
        mavenLocal()
    }
}

dependencies {
    // Compose
    implementation(libs.bundles.compose.impl)
    debugImplementation(libs.bundles.compose.debug.impl)

    implementation(libs.bundles.compose.extras)

    testImplementation(kotlin("test"))
    testImplementation(libs.kotlinx.coroutines.test)
    testImplementation(libs.turbine)
}