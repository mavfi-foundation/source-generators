FROM mcr.microsoft.com/devcontainers/dotnet:1-8.0

RUN set -ex; \
    \
    apt-get update; \
    apt-get install -y --no-install-recommends \
        mono-devel \
        nodejs \
        libnss3 \
        libnspr4 \
        libdbus-1-3 \
        libatk1.0-0 \
        libatk-bridge2.0-0 \
        libatspi2.0-0 \
        libxcomposite1 \
        libxdamage1 \
        libxfixes3 \
        libxrandr2 \
        libgbm1 \
        libxkbcommon0 \
        libasound2 \ 
    ; \
    apt-get purge -y --auto-remove -o APT::AutoRemove::RecommendsImportant=false $fetchDeps; \
    rm -rf /var/lib/apt/lists/*;