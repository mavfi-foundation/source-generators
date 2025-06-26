FROM mcr.microsoft.com/devcontainers/dotnet:1-8.0

RUN set -ex; \
    \
    apt-get update; \
    apt-get install -y --no-install-recommends \
        mono-devel \
    ; \
    apt-get purge -y --auto-remove -o APT::AutoRemove::RecommendsImportant=false $fetchDeps; \
    rm -rf /var/lib/apt/lists/*